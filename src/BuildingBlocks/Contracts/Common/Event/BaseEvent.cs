using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Contracts.Common.Event
{
    [NotMapped]
    public class BaseEvent : INotification
    {
        
    }
}