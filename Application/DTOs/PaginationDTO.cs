namespace Consorcio_Api.Application.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;

        private int recordsByPage = 10;
        private readonly int maxAmountRecordsByPage = 50;

        public int RecordsByPage
        {
            get { return recordsByPage; }
            set
            {
                recordsByPage = (value > maxAmountRecordsByPage) ? maxAmountRecordsByPage : value;
            }
        }
    }
}