using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
	[ServiceContract]
	public interface IService1
	{
		[OperationContract]
		List<DadosContato> ContatoListar();
		[OperationContract]
		List<DadosEvento> EventoListar();

		//// Contato
		[OperationContract]
		DadosContato ContatoAbrir(string numero);
		[OperationContract]
		void ContatoAdicionar(string nome, string numero, string uri);
		[OperationContract]
		void ContatoAtualizar(string nome, string numero, string uri);
		[OperationContract]
		List<DadosEvento> ContatoEventos(string numero);

		//// Evento
		[OperationContract]
		DadosEvento EventoAbrir(int id);
		[OperationContract]
		void EventoAdicionar(string nome, string descricao, DateTime data, string numero_responsavel, double latitude, double longitude);
		[OperationContract]
		void EventoAtualizar(int evento_id, string nome, string descricao, DateTime data, double latitude, double longitude);
		[OperationContract]
		void EventoDelete(int evento_id);
		[OperationContract]
		void EventoConvidarParticipante(int evento_id, string numero_participante);
		[OperationContract]
		void EventoRemoverParticipante(int evento_id, string numero_participante);

		//// Convite
		[OperationContract]
		DadosEvento ConviteAbrir(int id);
		[OperationContract]
		List<DadosConvite> ConviteListar(string numeroContato);
		[OperationContract]
		void ConviteAceitar(int idConvite);
		[OperationContract]
		void ConviteRecusar(int idConvite);

		// TODO: Add your service operations here
	}


	// Use a data contract as illustrated in the sample below to add composite types to service operations.
	[DataContract]
	public class CompositeType
	{
		bool boolValue = true;
		string stringValue = "Hello ";

		[DataMember]
		public bool BoolValue
		{
			get { return boolValue; }
			set { boolValue = value; }
		}

		[DataMember]
		public string StringValue
		{
			get { return stringValue; }
			set { stringValue = value; }
		}
	}

	[DataContract]
	public class DadosContato
	{
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string nome { get; set; }
		[DataMember]
		public string numero { get; set; }
		[DataMember]
		public string uri { get; set; }
		[DataMember]
		public List<DadosEvento> eventos { get; set; }
		
		public DadosContato serializavel()
		{
			DadosContato contato = new DadosContato();
			contato.id = id;
			contato.nome = nome;
			contato.numero = numero;
			contato.uri = uri;
			contato.eventos = new List<DadosEvento>();
            return contato;
		}
	}

	[DataContract]
	public class DadosConvite
	{
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string numeroConvidado { get; set; }
		[DataMember]
		public int idEvento { get; set; }
		[DataMember]
		public string nomeEvento { get; set; }

		public DadosConvite serializavel()
		{
			DadosConvite convite = new DadosConvite();
			convite.id = id;
			convite.numeroConvidado = numeroConvidado;
			convite.idEvento = idEvento;
			convite.nomeEvento = nomeEvento;
            return convite;
		}
	}

	[DataContract]
	public class DadosEvento
	{
		[DataMember]
		public int id { get; set; }
		[DataMember]
		public string nome { get; set; }
		[DataMember]
		public string descricao { get; set; }
		[DataMember]
		public DateTime data { get; set; }
		[DataMember]
		public double latitude { get; set; }
		[DataMember]
		public double longitude { get; set; }
		[DataMember]
		public List<DadosContato> participantes { get; set; }

		public DadosEvento serializavel()
		{
			DadosEvento evento = new DadosEvento();
			evento.id = id;
			evento.nome = nome;
			evento.descricao = descricao;
			evento.data = data;
			evento.latitude = latitude;
			evento.longitude = longitude;
			evento.participantes = new List<DadosContato>();
            return evento;
		}
	}
}
