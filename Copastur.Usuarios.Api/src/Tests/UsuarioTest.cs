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
namespace Tests;

public class UsuarioTest
{

    [Fact]
    public async void Get_OnError_ReturnsStatus_False()
    {
        DbContextOptions<PostgresContext> options = new DbContextOptions<PostgresContext>();
        var context = new PostgresContext(options);
        var configuration = new Mock<IConfiguration>();
        var producer = new RabbitMQProducer(configuration.Object);
        var service = new UsuarioService(context, producer);

        var response = await service.Read();

        Assert.Equal("False", response.Status.ToString());
    }
}