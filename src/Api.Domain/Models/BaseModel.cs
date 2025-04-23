using System;

namespace Domain.Models
{
    public class BaseModel
    {
        private long _Id;

        public long Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private DateTime _createAt;

        public DateTime CreateAt
        {
            get { return _createAt; }
            set
            {
                _createAt = value == null ? DateTime.UtcNow : value;
            }
        }

        private DateTime _updateAt;

        public DateTime UpdateAt
        {
            get { return _updateAt; }
            set { _updateAt = value; }
        }
    }
}
