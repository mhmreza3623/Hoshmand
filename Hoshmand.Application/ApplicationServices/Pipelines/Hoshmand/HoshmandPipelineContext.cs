using Hoshmand.Core.Dto.Response;
using Hoshmand.Core.Entities;
using Hoshmand.Core.Pipeline;

namespace Hoshmand.Application.ApplicationServices.Pipelines.Hoshmand
{
    public class HoshmandPipelineContext : PipelineContext<HoshmandPipelineRequest, HoshmandPipelineResponse>
    {
        public OrderRequestEntity OrderRequest { get; set; }
        public HoshmandResponseDto CheckNumPhoneResponse { get; set; }
        public HoshmandResponseDto CheckCodeResponse { get; internal set; }
        public HoshmandIdCardResponseDto IdCardRespone { get; internal set; }
    }
}