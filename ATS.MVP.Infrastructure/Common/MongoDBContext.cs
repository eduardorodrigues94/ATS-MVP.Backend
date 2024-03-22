using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ATS.MVP.Infrastructure.Configuration;
using ATS.MVP.Domain.Candidates;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using ATS.MVP.Domain.Candidates.DTOs;

namespace ATS.MVP.Infrastructure.Common;

public class MongoDBContext : IMongoDBContext
{
    private readonly IMongoDatabase _database;

    public MongoDBContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration[$"{nameof(MongoDBConfiguration)}:{nameof(MongoDBConfiguration.ConnectionString)}"]);

        _database = client.GetDatabase(configuration[$"{nameof(MongoDBConfiguration)}:{nameof(MongoDBConfiguration.Database)}"]);
    }

    public IMongoCollection<CandidateDTO> Candidates => _database.GetCollection<CandidateDTO>(nameof(Candidate));

    public static void ConfigureMappings()
    {
        ConventionRegistry.Register("CamelCaseConvention", new ConventionPack { new CamelCaseElementNameConvention() }, t => true);

        BsonClassMap.RegisterClassMap<CandidateDTO>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(c => c.Id)
                .SetIdGenerator(new GuidGenerator())
                .SetSerializer(new GuidSerializer(BsonType.String));
        });
    }
}
