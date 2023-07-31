using Hoshmand.Core.Common;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Hoshmand.Core.Interfaces.DomainServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Repositories;
using Hoshmand.Core.Interfaces.Shared;
using Hoshmand.Infrastructure.DomainService;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Hoshmand.Infrastructure.ExternalServices
{
    public class HoshmandClientProxy : IHoshmandClientProxy
    {
        private readonly IHttpClientUtility _httpClient;
        private readonly IGeneralRepository<OrderRequestEntity> _orderRepo;
        private readonly IGeneralRepository<NumPhoneRequestEntity> _numPhoneRepo;
        private readonly IGeneralRepository<CheckCodeRequestEntity> _checkCodeRepo;
        private readonly IGeneralRepository<IdCardRequestEntity> _idCardRepo;

        public HoshmandClientProxy(
            IHttpClientUtility httpClient,
            IHoshmandClientProxy hoshmandServiceProxy,
        IServiceSettings serviceSettings,
        IGeneralRepository<OrderRequestEntity> orderRepo,
        IGeneralRepository<NumPhoneRequestEntity> numPhoneRepo,
        IGeneralRepository<CheckCodeRequestEntity> checkCodeRepo,
        IGeneralRepository<IdCardRequestEntity> idCardRepo)
        {
            this._httpClient = httpClient;
            _orderRepo = orderRepo;
            _numPhoneRepo = numPhoneRepo;
            _checkCodeRepo = checkCodeRepo;
            _idCardRepo = idCardRepo;
        }


        //Hoshmand services
        public async Task<OrderRequestEntity> GetOrder()
        {
            var order = _orderRepo.Add(new OrderRequestEntity
            {
                OrderId = Guid.NewGuid().ToString()
            });


            var response = await _httpClient
                .SendJsonRequestAsync(
                HttpMethod.Post,
                "/GetOrderId2/",
                new OrderRequestDto { orderId = order.OrderId },
               CallbackHandler<HoshmandOrderResponseDto>,
                string.Empty);

            UpdateResponse(_orderRepo, order, response);

            if (response != null && response.Status.ToLower() == "inprogress")
            {
                return order;
            }

            return null;

        }

        public async Task<HoshmandResponseDto> CheckNumPhone(string mobile, string natinalCode, int orderRequestId, string orderId)
        {
            var numPhoneRequest = _numPhoneRepo.Add(new NumPhoneRequestEntity
            {
                OrderRequestId = orderRequestId,
                Number = natinalCode,
                Phone = mobile
            });


            var response = await _httpClient
                .SendJsonRequestAsync(
                HttpMethod.Put,
                $"/PostIdNumPhone2/{orderId}",
                new NumPhoneRequestDto { phone = mobile, idNum = natinalCode },
                          CallbackHandler<HoshmandResponseDto>,
                string.Empty);

            UpdateResponse(_numPhoneRepo, numPhoneRequest, response);

            if (response != null &&
                !string.IsNullOrWhiteSpace(response.RejectMessage))
            {
                return response;

            }

            return null;

        }

        public async Task<HoshmandResponseDto> CheckCode(int orderRequestId, string orderId, string messageCodeOutput)
        {
            var checkCodeRequest = _checkCodeRepo.Add(new CheckCodeRequestEntity
            {
                MessageCodeInput = messageCodeOutput,
                OrderRequestId = orderRequestId
            });

            var response = await _httpClient
                .SendJsonRequestAsync<CheckCodeRequestDto, HoshmandResponseDto>(
                HttpMethod.Put,
                $"/CheckCode2/{orderId}/",
                new CheckCodeRequestDto { messageCodeInput = messageCodeOutput },
                CallbackHandler<HoshmandResponseDto>,
                string.Empty);

            UpdateResponse(_checkCodeRepo, checkCodeRequest, response);

            if (response != null && response.Status.ToLower() == "inprogress")
            {
                return response;
            }

            return null;
        }

        public async Task<HoshmandIdCardResponseDto> IdCard(int orderRequestId, string orderId, IFormFile idCardLink, IFormFile idCardLink2)
        {
            var idCard = _idCardRepo.Add(new IdCardRequestEntity
            {
                CreateDate = DateTime.Now,
                ImageId1 = idCardLink.ToByteArray(),
                ImageId2 = idCardLink2.ToByteArray(),
                OrderRequestId = orderRequestId,
            });


            var idCard1 = new StreamContent(idCardLink.OpenReadStream());
            var idCard2 = new StreamContent(idCardLink2.OpenReadStream());

            var request = new List<FormDataRequestDto>() {
                    new FormDataRequestDto {
                        FileName = idCardLink.FileName,
                        ContentStream = idCard1,
                        FormFieldName = "idCardLink",
                        ContentType = idCardLink.ContentType },

        new FormDataRequestDto {
                        FileName = idCardLink2.FileName,
                        ContentStream = idCard2,
                        FormFieldName = "idCardLink2",
                        ContentType = idCardLink2.ContentType }
        };

            var response = await _httpClient.SendFormDataRequestAsync(
                HttpMethod.Put,
                $"/IDCard2/{orderId}",
                request,
                CallbackHandler<HoshmandIdCardResponseDto>);

            UpdateResponse(_idCardRepo, idCard, response);

            if (response != null && response.Status.ToLower() == "inprogress")
            {
                return response;
            }

            return null;

        }

        public async Task<CompareFaceResponseDto> CompareIdcardFace2(int orderRequestId, string orderId, IFormFile faceImage, IFormFile video)
        {
            var faceImageContent = new StreamContent(faceImage.OpenReadStream());
            var videoContent = new StreamContent(video.OpenReadStream());

            var request = new List<FormDataRequestDto>() {
                    new FormDataRequestDto {
                        FileName = faceImage.FileName,
                        ContentStream = faceImageContent,
                        FormFieldName = "faceImageLink",
                        ContentType = faceImage.ContentType },

        new FormDataRequestDto {
                        FileName = video.FileName,
                        ContentStream = videoContent,
                        FormFieldName = "videoLink",
                        ContentType = video.ContentType }
        };

            var response = await _httpClient.SendFormDataRequestAsync(
                HttpMethod.Put,
                $"/CompareIdcardFace2/{orderId}",
                request,
                CallbackHandler<CompareFaceResponseDto>);


            if (response != null && response.Status.ToLower() == "approve")
            {
                return response;
            }

            return null;
        }

        private TResult CallbackHandler<TResult>(object arg)
        {
            return JsonSerializer.Deserialize<TResult>(((HttpResponseMessage)arg).Content.ReadAsStreamAsync().Result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }

        private void UpdateResponse<TInput>(IGeneralRepository<TInput> repository, TInput entity, object response) where TInput : BaseEntity
        {
            entity.RawResponse = JsonSerializer.Serialize(response);
            entity.ResponsDate = DateTime.Now;
            repository.Udate(entity);
        }

    }
}
