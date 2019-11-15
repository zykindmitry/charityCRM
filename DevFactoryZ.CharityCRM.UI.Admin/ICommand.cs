namespace DevFactoryZ.CharityCRM.UI.Admin
{
    interface ICommand
    {
        string Help { get; }

        bool Recognize(string command);

        void Execute(string[] parameters);
    }
}
