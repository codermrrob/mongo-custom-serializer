using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;

namespace Json.Curation.Service
{
    public class JSchemaSerializer : SerializerBase<JSchema>
    {
        public override JSchema Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            BsonDocument rSchema = BsonDocumentSerializer.Instance.Deserialize(context);

            string rSchemaStr = rSchema.ToString();

            //Optionally set one or more custom validations
            JSchemaReaderSettings settings = new JSchemaReaderSettings
            {
                Validators = new List<JsonValidator> { new CustomValidator() }
            };

            return JSchema.Parse(rSchemaStr.Replace("`schema", "$schema")
                                            .Replace("`id", "$id")
                                            .Replace("`ref", "$ref"), settings);
        }
    }
}
 
