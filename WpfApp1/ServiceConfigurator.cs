using Microsoft.Extensions.DependencyInjection;
using WpfApp1.Interfaces;
using WpfApp1.Models.Operations;

namespace WpfApp1.Services
{
    class ServiceConfigurator
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            // Регистрируем операции
            serviceCollection.AddSingleton<IBinaryOperation, AddOperation>();
            serviceCollection.AddSingleton<IBinaryOperation, SubtractOperation>();
            serviceCollection.AddSingleton<IBinaryOperation, MultiplyOperation>();
            serviceCollection.AddSingleton<IBinaryOperation, DivideOperation>();
            serviceCollection.AddSingleton<IBinaryOperation, ModulusOperation>();

            serviceCollection.AddSingleton<IUnaryOperation, SqrtOperation>();
            serviceCollection.AddSingleton<IUnaryOperation, PowerOperation>();
            serviceCollection.AddSingleton<IUnaryOperation, OneDivideXOperation>();

            // Регистрируем калькулятор
            serviceCollection.AddSingleton<ICalculator, Calculator>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
