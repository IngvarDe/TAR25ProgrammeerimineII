using Microsoft.Extensions.DependencyInjection;
using ShopTARpe25.ApplicationServices.Services;
using ShopTARpe25.Core.ServiceInterface;
using ShopTARpe25.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopTARpe25.RealEstateTest.Macros;


namespace ShopTARpe25.RealEstateTest
{
    //iga koodirida ära kommenteerida, et mis mida meetod teeb ja miks seda on vaja?
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; set; }

        protected TestBase()
        {
            //loob uue teenuse kogu vajalikuks teenuste testimiseks
            var services = new ServiceCollection();
            //kutsub välja meetodi, mis lisab teenused ja vajalikud sõltuvused
            SetupServices(services);
            //selle kaudu saab teenuseid küsida
            ServiceProvider = services.BuildServiceProvider();
        }

        public virtual void SetupServices(IServiceCollection services)
        {
            services.AddScoped<IRealEstateServices, RealEstateServices>();

            //registreerib andmebaasi konteksti
            services.AddDbContext<ShopTARpe25Context>(x =>
            {
                //seadistab kasutama mälusisest andmebaasi nimega TEST, et testimisel kasutada 
                //ilma päris andmebaasita
                x.UseInMemoryDatabase("TEST");

                //eirab hoiatust, et mälusisene DB ei toeta tehinguid samamoodi nagu päris andmebaas
                x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            //kutsub välja macrode klassi
            RegisterMacros(services);
        }

        //vabastab ressursid peale kasutamist
        public void Dispose()
        {

        }

        //abimeetod, millega klass ja selle alamklassid saavad teenuse tööbi järgi küsida
        protected T Svc<T>()
        {
            //otsib teenusepakkuja tüüpi
            return ServiceProvider.GetService<T>();
        }

        //abimeetod makrode leidmsieks ja registreerimiseks
        private void RegisterMacros(IServiceCollection services)
        {
            //saaks reflektsiooniga otsingus kasutada
            var macroBaseType = typeof(IMacros);

            //loeb kokku kõik tüübid sellest assemblyst, kus Macro on määratletud
            var macros = macroBaseType.Assembly.GetTypes()
                //valib tööbid, mis rakendavad IMacros liidest, aga ei ole ise liidesed
                //ega abstraktsed klassid
                .Where(t => macroBaseType.IsAssignableFrom(t)
                && !t.IsInterface && !t.IsAbstract);

            //käib leitud makrode classid ükshaaval läbi
            foreach (var macro in macros)
            {
                //kasutab ühte makro eksemplaari
                services.AddSingleton(macro);
            }
        }
    }
}
