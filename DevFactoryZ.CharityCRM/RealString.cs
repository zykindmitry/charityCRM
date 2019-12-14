namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет валидируюемую строку 
    /// </summary>
    public class RealString
    {
        private readonly bool required;

        private readonly int maxLength;

        private readonly string name;

        private string value;

        public RealString(int maxLength, bool required, string name)
        {
            this.required = required;
            this.maxLength = maxLength;
            this.name = name;
        }

        public string Value
        {
            get 
            { 
                return value; 
            }
            set
            {
                if (required && string.IsNullOrWhiteSpace(value))
                {
                    throw new ValidationException($"{name} является обязательным аттрибутом");
                }

                if (value?.Length > maxLength)
                {
                    throw new ValidationException($"Значение {value} превышает максимально допустимое для {name}");
                }

                this.value = value;
            }
        }
    }
}
