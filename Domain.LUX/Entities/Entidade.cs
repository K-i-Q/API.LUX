using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.LUX.Entities
{
    public abstract class Entidade
    {
        public Entidade() 
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
    }
}
