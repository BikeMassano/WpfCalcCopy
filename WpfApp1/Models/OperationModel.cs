namespace WpfApp1.Models
{
    /// <summary>
    /// Модель для хранения данных об операциях
    /// </summary>
    class OperationModel
    {
        public double? FirstOperand { get; set; }
        public double? SecondOperand { get; set; }
        public string? Operation {  get; set; } = string.Empty;
    }
}
