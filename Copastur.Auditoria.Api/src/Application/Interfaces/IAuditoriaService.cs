using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain.DTOs.Requests;
using Domain.Models;

namespace Application.Interfaces;

public interface IAuditoriaService
{
    Task<bool> Create(AuditoriaMessageDto dto);
    Task<List<Auditoria>> Read();
}
