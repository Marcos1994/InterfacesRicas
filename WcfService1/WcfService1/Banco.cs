using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfService1
{
	static class Banco
	{
		private static int idContatoCount;
		private static int idEventoCount;
		private static int idConviteCount;

		public static List<DadosContato> Contatos { get; private set; }
		public static List<DadosEvento> Eventos { get; private set; }
		public static List<DadosConvite> Convites { get; private set; }

		static Banco()
		{
			idContatoCount = idEventoCount = idConviteCount = 1;
			Contatos = new List<DadosContato>();
			Eventos = new List<DadosEvento>();
			Convites = new List<DadosConvite>();

			CadastrarContato("Marcos", "987220984", "123");
			CadastrarContato("André", "36613516", "456");
			CadastrarContato("Cristiane", "12345", "789");
			CadastrarContato("Gabriel", "67890", "012");
			CadastrarEvento("Teste", "huehueBrBr", DateTime.Now, "987220984", 0, 0);
			CadastrarConvite("36613516", 1);
			ConfirmarConvite(1);
			CadastrarEvento("Outro teste", "asd", DateTime.Now, "36613516", 0, 0);
			CadastrarConvite("987220984", 2);
			CadastrarEvento("Testando", "hiahiahia", DateTime.Now, "987220984", 0, 0);
		}

		public static DadosContato CadastrarContato(string nome, string numero, string uri)
		{
			DadosContato novoContato = new DadosContato();
            try
			{
				novoContato = Contatos.First(c => c.numero == numero);
            }
			catch
			{
				novoContato = new DadosContato();
				novoContato.id = idContatoCount++;
				novoContato.nome = nome;
				novoContato.numero = numero;
				novoContato.uri = uri;
				novoContato.eventos = new List<DadosEvento>();

				Contatos.Add(novoContato);
			}
			return novoContato;
		}

		public static DadosEvento CadastrarEvento(string nome, string descricao, DateTime data, string numeroResponsavel, double latitude, double longitude)
		{
			DadosContato responsavel = Contatos.Find(c => c.numero == numeroResponsavel);
			if (responsavel == null)
				throw new Exception();

			DadosEvento novoEvento = new DadosEvento();
			novoEvento.id = idEventoCount++;
			novoEvento.nome = nome;
			novoEvento.descricao = descricao;
			novoEvento.data = data;
			novoEvento.latitude = latitude;
			novoEvento.longitude = longitude;
			novoEvento.participantes = new List<DadosContato>();
			addParticipante(responsavel, novoEvento);

			Eventos.Add(novoEvento);
			return novoEvento;
        }

		public static DadosConvite CadastrarConvite(string numeroConvidado, int idEvento)
		{
			DadosConvite convite = Convites.Find(c => c.idEvento == idEvento && c.numeroConvidado == numeroConvidado);
			if(convite != null)
				return convite;

			DadosContato contato = Contatos.Find(c => c.numero == numeroConvidado);
			DadosEvento evento = Eventos.Find(e => e.id == idEvento);
			if (evento == null || contato == null)
				throw new Exception();

			if (contato.eventos.Contains(evento) || evento.participantes.Contains(contato))
				return null;

			DadosConvite novoConvite = new DadosConvite();
			novoConvite.id = idConviteCount++;
			novoConvite.idEvento = idEvento;
			novoConvite.numeroConvidado = numeroConvidado;
			novoConvite.nomeEvento = evento.nome;
			Convites.Add(novoConvite);
			return novoConvite;
        }

		public static void AtualizarContato(string nome, string numero, string uri)
		{
			DadosContato contato = Contatos.Find(c => c.numero == numero);
			if (contato == null)
				throw new Exception();
			
			contato.nome = nome;
			contato.numero = numero;
			contato.uri = uri;
		}

		public static void AtualizarEvento(int id, string nome, string descricao, DateTime data, double latitude, double longitude)
		{
			DadosEvento evento = Eventos.Find(e => e.id == id);
			if (evento == null)
				throw new Exception();

			evento.nome = nome;
			evento.descricao = descricao;
			evento.data = data;
			evento.latitude = latitude;
			evento.longitude = longitude;

			List<DadosConvite> convites = (from c in Convites where c.idEvento == id select c).ToList();
			foreach (DadosConvite convite in convites)
				convite.nomeEvento = nome;
		}

		public static void ExcluirEvento(int id)
		{
			DadosEvento evento = Eventos.Find(e => e.id == id);
			if (evento != null)
			{
				Eventos.Remove(evento);

				foreach (DadosContato contato in evento.participantes)
					contato.eventos.Remove(evento);

				List<DadosConvite> convites = (from c in Convites where c.idEvento == id select c).ToList();
                foreach (DadosConvite convite in convites)
					Convites.Remove(convite);
			}
		}

		public static void RemoverParticipante(int id, string numeroParticipante)
		{
			DadosEvento evento = Eventos.Find(e => e.id==id);
			DadosContato participante = Contatos.Find(p => p.numero==numeroParticipante);
			if (evento == null || participante == null)
				throw new Exception();

			participante.eventos.Remove(evento);
            evento.participantes.Remove(participante);

			if (evento.participantes.Count == 0)
				ExcluirEvento(id);
		}

		public static void ConfirmarConvite(int idConvite, bool resposta = true)
		{
			DadosConvite convite = Convites.Find(c => c.id == idConvite);
			if (convite == null)
				throw new Exception();

			if (resposta)
				addParticipante(Contatos.Find(c => c.numero == convite.numeroConvidado), Eventos.Find(e => e.id == convite.idEvento));
            Convites.Remove(convite);
		}
		/*--------- Funções Internas ---------*/

		private static void addParticipante(DadosContato participante, DadosEvento evento)
		{
			participante.eventos.Add(evento);
			evento.participantes.Add(participante);
        }
	}
}
