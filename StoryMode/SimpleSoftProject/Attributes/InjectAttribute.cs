namespace SimpleSoftProject.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        public InjectAttribute()
        {           
        }
    }
}
