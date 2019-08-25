using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class SQLAccess : IDisposable
    {
        HackatonGSEntities DataAccesContext = new HackatonGSEntities();

        public void Dispose()
        {
            DataAccesContext.Dispose();
        }
        public IQueryable<Area> QueryAreas()
        {
            return DataAccesContext.Areas;
        }
        public int AddAreas(IEnumerable<Area> paramAreas)
        {
            DataAccesContext.Areas.AddRange(paramAreas);
            return DataAccesContext.SaveChanges();
        }
        public int AddArea(Area paramArea)
        {
            DataAccesContext.Areas.Add(paramArea);
            return DataAccesContext.SaveChanges();
        }
        public int UpdateArea(Area paramArea)
        {
            DataAccesContext
                .Areas
                .Where(a => a.Id == paramArea.Id)
                .ToList()
                .ForEach(a => a.Nombre = paramArea.Nombre);
            return DataAccesContext.SaveChanges();
        }
        public int DeleteArea(Area paramArea)
        {
            DataAccesContext.Areas.Remove(paramArea);
            return DataAccesContext.SaveChanges();
        }

        public IQueryable<Template> GetTemplates() 
        {
            return DataAccesContext.Templates;
        }
        public int AddTemplate(Template paramTemplate)
        {
            DataAccesContext.Templates.Add(paramTemplate);
            return DataAccesContext.SaveChanges();
        }
        public int UpdateTemplate(Template paramTemplate)
        {
            DataAccesContext
                .Templates
                .Where(t =>
                    t.IdArea == paramTemplate.IdArea &&
                    t.Id == paramTemplate.Id)
                .ToList()
                .ForEach(t =>
                {
                    t.Nombre = paramTemplate.Nombre;
                    t.Contenido = paramTemplate.Contenido;
                    t.Fecha = DateTime.Now;
                });
            return DataAccesContext.SaveChanges();
        }
        public int DeleteTemplate(Template paramTemplate)
        {
            DataAccesContext.Templates.Remove(paramTemplate);
            return DataAccesContext.SaveChanges();
        }

        public IQueryable<Ticket> GetTickets() 
        {
            return DataAccesContext.Tickets;
        }
        public int AddTicket(Ticket paramTicket)
        {
            DataAccesContext.Tickets.Add(paramTicket);
            return DataAccesContext.SaveChanges();
        }
        public int UpdateTicket(Ticket paramTicket)
        {
            DataAccesContext
                .Tickets
                .Where(t => t.Id == paramTicket.Id)
                .ToList()
                .ForEach(t =>
                {
                    t.Fecha = DateTime.Now;
                    t.Descripcion = paramTicket.Descripcion;
                });
            return DataAccesContext.SaveChanges();
        }

        public int DeleteTicket(Ticket paramTicket) 
        {
            DataAccesContext.Tickets.Remove(paramTicket);
            return DataAccesContext.SaveChanges();
        }

        public IQueryable<ValoresTicket> GetValoresByTicket(Ticket paramTicket) 
        {
            return DataAccesContext.ValoresTickets.Where(v => v.IdTicket == paramTicket.Id);
        }
        public IQueryable<ValoresTicket> GetValoresByIdTicket(long idTicket) 
        {
            return DataAccesContext.ValoresTickets.Where(v => v.IdTicket == idTicket);
        }
        public int AddValorTicket(ValoresTicket paramValor) 
        {
            DataAccesContext.ValoresTickets.Add(paramValor);
            return DataAccesContext.SaveChanges();
        }

        public int AddValoresTicket(IEnumerable<ValoresTicket> paramValores) 
        {
            DataAccesContext.ValoresTickets.AddRange(paramValores);
            return DataAccesContext.SaveChanges();
        }
        public int UpdateValorTicket(ValoresTicket paramValor) 
        {
            DataAccesContext.ValoresTickets.Where(v => v.IdTicket == paramValor.IdTicket).ToList().ForEach(v => v.Valor = paramValor.Valor);
            return DataAccesContext.SaveChanges();
        }
        public int DeleteValorTicket(ValoresTicket paramValor) 
        {
            DataAccesContext.ValoresTickets.Remove(paramValor);
            return DataAccesContext.SaveChanges();
        }
        private Dictionary<string, string> GetDictionaryFromValores(IQueryable<ValoresTicket> Valores)
        {
            var result = new Dictionary<string, string>();
            foreach (var i in Valores) 
            {
                result.Add(i.Clave, i.Valor);
            }
            return result;
        }
        public Dictionary<string, string> GetDictionaryFromIdTicket(long idTicket)
        {
            return GetDictionaryFromValores(DataAccesContext.ValoresTickets.Where(v=> v.IdTicket ==idTicket));
        }
    }

}
