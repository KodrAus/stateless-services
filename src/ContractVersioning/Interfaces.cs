using System;

namespace ContractVersioning
{
    public interface ISerialize
    {
        byte[] Serialize();
    }

    public interface IDeserialize<TSelf>
    {
        TSelf Deserialize(Span<byte> data);
    }

    public interface IContract<T>
    {
        int Version { get; }
        T Data { get; }
    }

    public interface IUpgrade<TSelf, TFrom>
    {
        TSelf From(TFrom upgradeFrom);
    }
}
