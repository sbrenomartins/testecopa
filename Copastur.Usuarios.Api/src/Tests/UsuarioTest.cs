using Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Domain.Models;
using Domain.DTOs.Responses;
using Domain.DTOs.Requests;
using Application.Services;
using Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class UsuarioTest
    {
        [Fact]
        public async void Get_OnSuccess_ReturnsStatusCode_200()
        {
            var mockService = new Mock<IUsuarioService>();
            var cont = new UsuarioController(mockService.Object);
            var result = (OkObjectResult)await cont.Get();

            Assert.Equal(result.StatusCode, 200);
        }
    }
}