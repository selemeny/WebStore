﻿using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services.InCookies
{
    public class CookiesCartService : ICartService
    {
        private readonly string cartName;;
        private readonly IProductData productData;
        private readonly HttpContextAccessor httpContextAccessor;

        public CookiesCartService(IProductData productData, HttpContextAccessor httpContextAccessor)
        {
            this.productData = productData;
            this.httpContextAccessor = httpContextAccessor;

            var user = httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated ? user.Identity.Name : null;

            cartName = $"Webstore.Cart[{userName}]";
        }


        Cart Cart
        {
            get 
            {
                var context = httpContextAccessor.HttpContext;
                var cookies = context.Response.Cookies;
                var cartCookie = context.Request.Cookies[cartName];
                if(cartCookie is null)
                {
                    var cart = new Cart();
                    cookies.Append(cartName, JsonConvert.SerializeObject(cart));
                    return cart;
                }

                ReplaceCookie(cookies, cartCookie);
                return JsonConvert.DeserializeObject<Cart>(cartCookie);
            }
            set => ReplaceCookie(httpContextAccessor.HttpContext.Response.Cookies, JsonConvert.SerializeObject(value));
        }


        void ReplaceCookie(IResponseCookies cookies, string cookie)
        {
            cookies.Delete(cartName);
            cookies.Append(cartName, cookie, new CookieOptions { Expires = DateTime.Now.AddDays(15) });
        }



        public void AddToCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id, Quantity = 1 });
            else
                item.Quantity++;

            Cart = cart;
        }

        public void DecrementFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;
            var item = cart.Items.FirstOrDefault(x => x.ProductId == id);

            if (item is null)
                return;

            cart.Items.Remove(item);

            Cart = cart;
        }

        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public CartViewModel TransformFromCart()
        {
            var products = productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(x => x.ProductId).ToList()
            });

            var productViewModel = products.ToView();

            return new CartViewModel
            {
                Items = Cart.Items.ToDictionary(
                    item => productViewModel.First(x => x.Id == item.ProductId),
                    item => item.Quantity
            };
        }
    }
}
