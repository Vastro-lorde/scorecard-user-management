namespace scorecard_user_mgt.Utilities
{
    public class PaginationModel<T>
    {
        public T PageItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
        public int PreviousPage { get; set; }
    }
}
