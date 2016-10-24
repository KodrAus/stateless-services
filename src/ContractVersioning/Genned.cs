using System;

namespace ContractVersioning
{
    /* START CODEGEN */

    public partial class MyContract : ISerialize, IDeserialize<MyContract>
    {
        public MyContract()
        {
            Version = 2;
        }

        public int Version { get; private set; }
        public MyContractV3 Data { get; private set; }

        public MyContract Deserialize(Span<byte> data)
        {
            var versionSpan = data.Slice(0, 4);
            var version = versionSpan.Read<int>();
            var dataSpan = data.Slice(4);

            switch (version)
            {
                case 1:
                    {
                        Data = new MyContractV1()
                            .Deserialize(data)
                            .Upgrade()
                            .Upgrade();
                        break;
                    }
                case 2:
                    {
                        Data = new MyContractV2()
                            .Deserialize(data)
                            .Upgrade();
                        break;
                    }
                case 3:
                    {
                        Data = new MyContractV3()
                            .Deserialize(data);
                        break;
                    }
            }

            throw new Exception("Unknown version");
        }

        public byte[] Serialize()
        {
            return Data.Serialize();
        }
    }

    public static class MyContractV1Extensions
    {
        public static MyContractV2 Upgrade(this MyContractV1 contract)
        {
            return new MyContractV2().From(contract);
        }
    }

    public static class MyContractV2Extensions
    {
        public static MyContractV3 Upgrade(this MyContractV2 contract)
        {
            return new MyContractV3().From(contract);
        }
    }

    /* END CODEGEN */
}
