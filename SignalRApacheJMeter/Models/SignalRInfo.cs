using System;

namespace SignalRApacheJMeter.Models
{
    public class SignalRInfo
    {
        #region Fields

        private Guid _id;
        private string _message;
        private DateTime _date;

        #endregion

        #region Properties

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public Guid Id { get => _id; set => _id = value; }

        /// <summary>
        /// Short Description
        /// </summary>
        public string Message { get => _message; set => _message = value; }

        /// <summary>
        /// Date of message
        /// </summary>
        public DateTime Date { get => _date; set => _date = value; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for the SignalRInfo object
        /// </summary>
        /// <param name="id">Unique Identifier</param>
        /// <param name="message">Message body</param>
        /// <param name="date">Date of Message</param>
        public SignalRInfo(Guid id, string message, DateTime date)
        {
            this.Id = id;
            this.Message = message;
            this.Date = date;
        }

        #endregion

        #region Methods

        #endregion

    }
}
