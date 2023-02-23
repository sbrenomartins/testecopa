using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

using Domain.DTOs.Requests;
using Domain.Models;

using Infra.Settings;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

namespace Application.Services
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IMongoCollection<Auditoria> _collection;

        public AuditoriaService(IOptions<AuditoriaSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<Auditoria>(options.Value.CollectionName);
        }

        public async Task<List<Auditoria>> Read()
        {
            var response = await _collection.Find(_ => true).ToListAsync();
            return response;
        }

        public async Task<bool> Create(AuditoriaMessageDto dto)
        {
            try
            {
                Auditoria auditoria = new Auditoria
                {
                    Content = dto.Content,
                    Date = dto.Date,
                    Method = dto.Method,
                };

                await _collection.InsertOneAsync(auditoria);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
