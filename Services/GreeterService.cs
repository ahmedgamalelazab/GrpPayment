using Grpc.Core;
using PaymentService;
using System.Linq;
namespace PaymentService.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;

    private readonly List<Product> products = new List<Product>(){
        new Product(){Id = 1 , Name = "PlayStation 5 Console" , Image = "https://m.media-amazon.com/images/I/619BkvKW35L._AC_UL320_.jpg" , Price=700},
        new Product(){Id = 2 , Name = "Meta Quest 2 Virtual Reality Headset" , Image = "https://m.media-amazon.com/images/I/61tE7IcuLmL._AC_UL320_.jpg" , Price=399},
        new Product(){Id = 3 , Name = "Xbox Series S" , Image = "https://m.media-amazon.com/images/I/71NBQ2a52CL._AC_UL320_.jpg" , Price=255},
        new Product(){Id = 4 , Name = "PlayStation DualSense Wireless" , Image = "https://m.media-amazon.com/images/I/61Uh8NFDzsL._AC_UL320_.jpg" , Price=22},
        new Product(){Id = 5 , Name = "DualShock 4 Wireless" , Image = "https://m.media-amazon.com/images/I/61IG46p-yHL._AC_UL320_.jpg" , Price=100},
        new Product(){Id = 6 , Name = "Logitech G502 HERO High Performance" , Image = "https://m.media-amazon.com/images/I/61mpMH5TzkL._AC_UY218_.jpg" , Price=10},
        new Product(){Id = 7 , Name = "BENGOO G9000 Stereo Gaming Headset" , Image = "https://m.media-amazon.com/images/I/61CGHv6kmWL._AC_UY218_.jpg" , Price=290},
    };

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override Task<HelloReply> PayItem(RequestLoad request, ServerCallContext context)
    {
        var targetItem = products.SingleOrDefault((product) => product.Id == request.ItemId);

        if (targetItem != null)
        {
            return Task.FromResult(new HelloReply
            {
                Message = $"Item : {targetItem.Name} Sold Successfully : with total Cost : {targetItem.Price}"
            });
        }

        return Task.FromResult(new HelloReply
        {
            Message = "failed to Pay The Item " + request.ItemId
        });
    }
}
