using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
	// NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
	public class Service1 : IService1
	{
		public List<DadosContato> ContatoListar()
		{
			List<DadosContato> contatos = new List<DadosContato>();
			foreach (DadosContato c in Banco.Contatos)
				contatos.Add(c.serializavel());
            return contatos;
		}

		public List<DadosEvento> EventoListar()
		{
			List<DadosEvento> eventos = new List<DadosEvento>();
			foreach (DadosEvento e in Banco.Eventos)
				eventos.Add(e.serializavel());
			return eventos;
		}
		
		/*Contatos*/

		public DadosContato ContatoAbrir(string numero)
		{
			try
			{
				return Banco.Contatos.Find(c => c.numero == numero).serializavel();
            }
			catch
			{
				//mensagem de usuario n encontrado
				return new DadosContato();
			}
		}

        public void ContatoAdicionar(string nome, string numero, string uri)
		{
			Banco.CadastrarContato(nome, numero, uri);
		}

		public void ContatoAtualizar(string nome, string numero, string uri)
		{
			try
			{
				Banco.AtualizarContato(nome, numero, uri);
			}
			catch
			{
				//mensagem de usuario n encontrado
			}
		}

		public List<DadosEvento> ContatoEventos(string numero)
		{
			List<DadosEvento> eventos = new List<DadosEvento>();
			DadosContato contato = Banco.Contatos.Find(c => c.numero == numero);
			if (contato == null)
			{
				//mensagem de usuario n encontrado
			}
			else foreach (DadosEvento e in contato.eventos)
				eventos.Add(e.serializavel());
			return eventos;
        }

		/*Eventos*/

		public DadosEvento EventoAbrir(int id)
		{
			try
			{
				DadosEvento evento = Banco.Eventos.Find(e => e.id == id);
				DadosEvento eventoSerializado = evento.serializavel();
				foreach (DadosContato p in evento.participantes)
					eventoSerializado.participantes.Add(p.serializavel());
                return eventoSerializado;
			}
			catch
			{
				//mensagem de evento n encontrado
				return new DadosEvento();
			}
		}

		public void EventoAdicionar(string nome, string descricao, DateTime data, string numero_responsavel, double latitude, double longitude)
		{
			try
			{
				Banco.CadastrarEvento(nome, descricao, data, numero_responsavel, latitude, longitude);
			}
			catch
			{
				//mensagem de usuario n encontrado
			}
		}

		public void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, double latitude, double longitude)
		{
			try
			{
				Banco.AtualizarEvento(evento_id, nome, descricao, data, latitude, longitude);
            }
			catch
			{
				//mensagem de evento não encontrado
			}
		}

		public void EventoDelete(int evento_id)
		{
			Banco.ExcluirEvento(evento_id);
		}

		public void EventoConvidarParticipante(int evento_id, string numero_participante)
		{
			try
			{
				Banco.CadastrarConvite(numero_participante, evento_id);
			}
			catch
			{
				//mensagem de contato ou evento não encontrado
			}
		}

		public void EventoRemoverParticipante(int evento_id, string numero_participante)
		{
			try
			{
				Banco.RemoverParticipante(evento_id, numero_participante);
			}
			catch
			{
				//mensagem de evento ou contato não encontrado
			}
		}

		/*Convites*/

		public DadosEvento ConviteAbrir(int id)
		{
			try
			{
				DadosConvite convite = Banco.Convites.Find(c => c.id == id);
				DadosEvento evento = Banco.Eventos.Find(e => e.id == convite.idEvento);
				DadosEvento eventoSerializado = evento.serializavel();
				foreach (DadosContato p in evento.participantes)
					eventoSerializado.participantes.Add(p.serializavel());
				return eventoSerializado;
			}
			catch
			{
				//mensagem de evento n encontrado
				return new DadosEvento();
			}
		}

		public List<DadosConvite> ConviteListar(string numeroContato)
		{
			try
			{
				List<DadosConvite> convites = (from c in Banco.Convites where c.numeroConvidado == numeroContato select c).ToList();
				return convites;
			}
			catch
			{
				//mensagem de usuario n encontrado
                return new List<DadosConvite>();
			}
		}

		public void ConviteAceitar(int idConvite)
		{
			try
			{
				Banco.ConfirmarConvite(idConvite);
			}
			catch
			{

			}
		}

		public void ConviteRecusar(int idConvite)
		{

			try
			{
				Banco.ConfirmarConvite(idConvite, false);
			}
			catch
			{

			}
		}
	}
}
