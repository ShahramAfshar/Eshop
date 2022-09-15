using EStore.Application.Interfaces.Contexts;
using EStore.Common.Dto;
using EStore.Domain.Entities.Carts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Application.Services.Carts
{
    public interface ICartService
    {
        ResultDto AddToCart(int productId, Guid browserId);
        ResultDto DeleteFromCart(int productId, Guid browserId);
        ResultDto<CartDto> GetMyCart(Guid browserId,int? userId);
        ResultDto Add(int cartItemId);
        ResultDto LowOff(int cartItemId);


    }

    public class CartService : ICartService
    {
        private readonly IDataBaseContext _context;
        public CartService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Add(int cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            cartItem.Count++;
            _context.SaveChanges();

            return new ResultDto() { 
            IsSuccess=true,
            Message=""
            };
        }

        public ResultDto AddToCart(int productId, Guid browserId)
        {
            var cart = _context.Carts.Where(c => c.BrowserId == browserId && c.Finished == false).FirstOrDefault();

            if (cart == null)
            {
                Cart newCart = new Cart()
                {
                    Finished = false,
                    BrowserId = browserId
                };
                _context.Carts.Add(newCart);
                _context.SaveChanges();

                cart = newCart;
            }

            var product = _context.Products.Find(productId);
            var cartItem = _context.CartItems.Where(c => c.ProductId == productId && c.CartId == cart.Id).FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.Count++;
            }

            CartItem newCartItem = new CartItem()
            {
                Cart = cart,
                Product = product,
                Price = product.Price,
                Count = 1,
            };
            _context.CartItems.Add(newCartItem);
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = $"محصول  {product.Name} با موفقیت به سبد خرید شما اضافه شد",
            };

        }

        public ResultDto DeleteFromCart(int productId, Guid browserId)
        {
            var cartItem = _context.CartItems.Where(c => c.Cart.BrowserId == browserId).FirstOrDefault();

            if (cartItem != null)
            {
                cartItem.IsRemove = true;
                cartItem.RemoveTime = DateTime.Now;
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = $"محصول با موفقیت از سبد خرید شما حذف شد"
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = $"محصول در سبد خرید شما وجود ندارد"
                };
            }


        }

        public ResultDto<CartDto> GetMyCart(Guid browserId, int? userId)
        {
            var cart = _context.Carts
                .Include(c => c.CartItem)
                 .ThenInclude(c => c.Product)
                 .ThenInclude(c=>c.ProductImages)
                .Where(c => c.BrowserId == browserId && c.Finished == false)
                .OrderByDescending(c => c.Id)
                .FirstOrDefault();

            if (cart ==null)
            {
                return new ResultDto<CartDto>() { 
                Data= new CartDto() { CartItemDtos= new List<CartItemDto>()},
                IsSuccess=true,
                Message=""
                
                };
            }


            if (userId!= null)
            {
                var user = _context.Users.Find(userId);
                cart.User = user;
                _context.SaveChanges();
            }

            if (cart !=null)
            {
                return new ResultDto<CartDto>()
                {
                    Data = new CartDto()
                    {
                        CartId=cart.Id,
                        ProductCount = cart.CartItem.Count(),
                        SumAmount = cart.CartItem.Sum(c => c.Price * c.Count),
                        CartItemDtos = cart.CartItem.Select(c => new CartItemDto()
                        {
                            Count = c.Count,
                            Price = c.Price,
                            ProductName = c.Product.Name,
                            Id = c.Id,
                            Image = c.Product?.ProductImages?.FirstOrDefault()?.SourceImage ?? ""

                        }).ToList()
                    },
                    IsSuccess = true,
                    Message = "",

                };
            }

            else
            {
                return new ResultDto<CartDto>() { 
                Data= new CartDto(),
                IsSuccess=false,
                Message=""
                
                };
            }
        }

        public ResultDto LowOff(int cartItemId)
        {
            var cartItem = _context.CartItems.Find(cartItemId);
            if (cartItem.Count <=1)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = ""
                };

            }

            cartItem.Count--;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }
    public class CartDto
    {
        public int CartId { get; set; }
        public int SumAmount { get; set; }
        public int ProductCount { get; set; }
        public List<CartItemDto> CartItemDtos { get; set; }

    }
    public class CartItemDto
    {
        public int Id { get; set; }
        public string  Image { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}
