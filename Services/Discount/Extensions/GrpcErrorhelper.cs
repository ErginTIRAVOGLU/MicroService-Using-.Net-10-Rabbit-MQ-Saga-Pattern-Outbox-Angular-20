using Google.Protobuf;
using Google.Rpc;
using static Google.Rpc.BadRequest.Types;

// ✅ Çakışmayı çözmek için global alias'ları eklendi
using RpcException = global::Grpc.Core.RpcException;
using StatusCode = global::Grpc.Core.StatusCode;
using Metadata = global::Grpc.Core.Metadata;
using Google.Protobuf.WellKnownTypes;

namespace Discount.Extensions;

public static class GrpcErrorhelper
{
    public static RpcException CreateValidationException(Dictionary<string, string> fieldErrors)
    {
        var fieldViolations = new List<FieldViolation>();
        foreach (var error in fieldErrors)
        {
            fieldViolations.Add(new FieldViolation
            {
                Field = error.Key,
                Description = error.Value
            });
        }

        var badRequest = new BadRequest();
        badRequest.FieldViolations.AddRange(fieldViolations);

        var status = new Google.Rpc.Status
        {
            Code = (int)StatusCode.InvalidArgument,
            Message = "Validation Failed",
            Details = { Any.Pack(badRequest) }
        };

        // ✅ Metadata doğru bulundu ve .Add() ile byte[] eklendi
        var trailers = new Metadata();
        trailers.Add("grpc-status-details-bin", status.ToByteArray());

        // ✅ Grpc.Core.Status için global:: kullanıldı
        return new RpcException(new global::Grpc.Core.Status(StatusCode.InvalidArgument, "Validation errors"), trailers);
    }
}