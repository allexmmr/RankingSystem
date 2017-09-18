using System.CodeDom.Compiler;
using System.Data.Entity.Migrations.Infrastructure;
using System.Resources;

namespace RankingSystem.Data.Migrations
{
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class Initial : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(Initial));

        string IMigrationMetadata.Id
        {
            get { return "20170916_Initial"; }
        }

        string IMigrationMetadata.Source
        {
            get { return null; }
        }

        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}