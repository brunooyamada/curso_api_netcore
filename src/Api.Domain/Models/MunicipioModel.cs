﻿namespace Domain.Models
{
    public class MunicipioModel : BaseModel
    {
		private string _nome;

		public string Nome
		{
			get { return _nome; }
			set { _nome = value; }
		}

		private int _codIBGE;

		public int CodIBGE
		{
			get { return _codIBGE; }
			set { _codIBGE = value; }
		}

		private long _ufId;

		public long UfId
		{
			get { return _ufId; }
			set { _ufId = value; }
		}


	}
}
