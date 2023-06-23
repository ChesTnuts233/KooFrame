using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace KooFrame.KooManagers.PoolManager
{
    public interface IPool<T>
{
    T Allocate();
    bool Recycle(T obj);
}

public abstract class Pool<T> : IPool<T>
{
    protected Stack<T> cacheStack = new Stack<T>();

    protected IObjectFactory<T> factory;

    public int CurCount => cacheStack.Count();

    //分配游戏物体
    public T Allocate()
    {
        return cacheStack.Count > 0 ? cacheStack.Pop() : factory.Create();
    }


    public abstract bool Recycle(T obj);
}

public class SimplyObjectPool<T> : Pool<T>
{
    private Action<T> _resetMethod;

    public SimplyObjectPool(Func<T> factoryMethod, Action<T> resetMethod = null, int initCount = 0)
    {
        factory = new CustomObjectFactory<T>(factoryMethod);
        _resetMethod = resetMethod;

        for (int i = 0; i < initCount; i++)
        {
            cacheStack.Push(factory.Create());
        }
    }

    public override bool Recycle(T obj)
    {
        if (_resetMethod != null)
        {
            _resetMethod(obj);
        }

        cacheStack.Push(obj);

        return true;
    }
}

public interface IObjectFactory<T>
{
    T Create();
}

public class CustomObjectFactory<T> : IObjectFactory<T>
{
    private Func<T> _factoryMethod;

    public CustomObjectFactory(Func<T> factoryMethod)
    {
        _factoryMethod = factoryMethod;
    }

    public T Create()
    {
        return _factoryMethod();
    }
}

#if UNITY_EDITOR
public class PoolExample
{
    class Fish{}

    [MenuItem("KooFramework/测试/PoolTest",false,1)]
    private static void PoolTest()
    {
        var fishPool = new SimplyObjectPool<Fish>(() => new Fish(), null, 100);
        Debug.LogFormat("fishPool.CurCount:{0}",fishPool.CurCount);
        var fishOne = fishPool.Allocate();
        Debug.LogFormat("fishPool.CurCount:{0}",fishPool.CurCount);
        fishPool.Recycle(fishOne);
        Debug.LogFormat("fishPool.CurCount:{0}",fishPool.CurCount);
        for (int i = 0; i < 10; i++)
        {
            fishPool.Allocate();  
        }
        Debug.LogFormat("fishPool.CurCount:{0}",fishPool.CurCount);
    }
}
#endif
}
