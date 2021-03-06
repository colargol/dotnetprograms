using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;
using DbTool.Lib.Syntax;

namespace DbTool.Lib
{
    public class MetaInfoProvider : IMetaInfoProvider
    {
        private readonly ITypeCache _typeCache;

        public MetaInfoProvider(ITypeCache typeCache)
        {
            _typeCache = typeCache;
        }

        public TagType GetTypeOf(string word)
        {
            var type = _typeCache.GetType(word);
            return type == null ? TagType.Nothing : TagType.Object;
        }

        public TableMeta GetType(string word)
        {
            return _typeCache.GetType(word);
        }
    }
}