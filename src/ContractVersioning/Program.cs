using System;
using System.Linq;

namespace ContractVersioning
{
    public partial class MyContract : IContract<MyContractV3>
    {

    }

    public class MyContractV1 : ISerialize, IDeserialize<MyContractV1>
    {
        public string Title { get; protected set; }

        public MyContractV1 Deserialize(Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public class MyContractV2 : MyContractV1, ISerialize, IDeserialize<MyContractV2>, IUpgrade<MyContractV2, MyContractV1>
    {
        public int Id { get; protected set; }

        public new MyContractV2 Deserialize(Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public MyContractV2 From(MyContractV1 upgradeFrom)
        {
            Id = -1;
            Title = upgradeFrom.Title;

            return this;
        }

        public new byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }

    public class MyContractV3 : ISerialize, IDeserialize<MyContractV3>, IUpgrade<MyContractV3, MyContractV2>
    {
        public int Id { get; protected set; }
        public string Description { get; protected set; }

        public MyContractV3 Deserialize(Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public MyContractV3 From(MyContractV2 upgradeFrom)
        {
            Id = upgradeFrom.Id;
            Description = "updated description";

            return this;
        }

        public byte[] Serialize()
        {
            throw new NotImplementedException();
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var data = BitConverter.GetBytes(1);
            data.Concat(System.Text.Encoding.ASCII.GetBytes("This is some input"));

            var contract = new MyContract().Deserialize(data.Slice());

            Console.WriteLine(contract.Data.Description);
            Console.ReadLine();
        }
    }
}
