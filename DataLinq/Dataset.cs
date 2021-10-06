using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Substrait.Protobuf;
using Expression = System.Linq.Expressions.Expression;
using Type = Substrait.Protobuf.Type;

namespace DataLinq
{
    public class Dataset<T>
    {
        public List<string> Paths { get; }
        public Type.Types.NamedStruct Schema { get; }

        public Dataset(List<string> paths)
        {
            Paths = paths;
            Schema = TypeParser.SchemaFromType<T>();
        }
        
        public RelBuilder Read()
        {
            var localFiles = new ReadRel.Types.LocalFiles();
            foreach (var path in Paths)
            {
                localFiles.Items.Add(new ReadRel.Types.LocalFiles.Types.FileOrFiles
                {
                    UriPath = path,
                    Format = ReadRel.Types.LocalFiles.Types.FileOrFiles.Types.Format.Parquet
                });
            }
            var readRel = new ReadRel
            {
                BaseSchema = Schema,
                Common = SubstraitUtil.SimpleRelCommon,
                Filter = SubstraitUtil.ExpressionTrue,
                Projection = SubstraitUtil.DefaultProjection,
                LocalFiles = localFiles
            };
            var plan = new PlanBuilder();
            var relBuilder = new RelBuilder(plan, new Rel {Read = readRel});
            plan.AddRel(relBuilder);
            return relBuilder;
        }

    }
}
