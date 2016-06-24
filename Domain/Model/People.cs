namespace CJSoftware.Domain.Model
{
    /// <summary>
    ///     Not sure if this should have been called "Person"
    /// </summary>
    public class People : DomainObject<int>
    {
        public string EmployeeReference { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}