namespace Temna_Doba.InputGroup
{
    public class InputReceiver
    {
        private bool hasReachedEOF = false;

        public int LinesRead { get; set; } = 0;

        public string GetNextInLine()
        {
            int inputValue = 0;
            string inputLine = "";

            while ((inputValue=Console.Read()) !=-1)
            {
                char inputChar = (char)inputValue;
                
                if (inputChar == '\n' || inputChar == '\r')
                {
                    break;
                }
                else
                {
                    inputLine += inputChar;
                }
            }
            if (inputValue ==-1)
            {
                hasReachedEOF = true;
            }
            else 
            {
                LinesRead++;
            }

            return inputLine;
        }

        public bool HasInputEnded()
        {
            return hasReachedEOF;
        }
    }
}