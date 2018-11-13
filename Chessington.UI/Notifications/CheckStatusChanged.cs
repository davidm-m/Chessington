using System.Configuration;

namespace Chessington.UI.Notifications
{
    public class CheckStatusChanged
    {
        public CheckStatusChanged(bool whiteInCheck, bool blackInCheck)
        {
            WhiteInCheck = whiteInCheck;
            BlackInCheck = blackInCheck;
            UpdateStatus();
        }

        private bool whiteInCheck;
        public bool WhiteInCheck {
            get
            {
                return whiteInCheck;
            }
            set
            {
                whiteInCheck = value;
                UpdateStatus();
            }
        }

        private bool blackInCheck;
        public bool BlackInCheck
        {
            get
            {
                return blackInCheck;
            }
            set
            {
                blackInCheck = value;
                UpdateStatus();
            }
        }

        public string Status { get; set; }

        private void UpdateStatus()
        {
            var status = "";
            if (WhiteInCheck)
            {
                status = status + "White is in check.\n";
            }

            if (BlackInCheck)
            {
                status = status + "Black is in check.\n";
            }

            Status = status;
        }
    }
}