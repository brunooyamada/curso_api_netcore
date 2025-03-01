using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
		[Key]
        public long Id { get; set; }
		private DateTime? _createAt;

		public DateTime? CreatedAt
		{
			get { return _createAt; }
			set { _createAt = (value == null ? DateTime.UtcNow : value); }
		}

        public DateTime? UpdateAt { get; set; }

    }
}
