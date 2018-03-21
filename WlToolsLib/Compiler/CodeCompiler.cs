using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace WlToolsLib
{
    /// <summary>
    /// 代码编译
    /// </summary>
    public class CodeCompiler
    {
        public List<string> ErrInfo { get; set; } = new List<string>();
        public CSharpCodeProvider Provider { get; protected set; }
        public CompilerParameters Parameters { get; protected set; }

        public CodeCompiler()
        {
            ResetCompiler();
        }

        public void ResetCompiler()
        {
            //1>实例化C#代码服务提供对象
            Provider = new CSharpCodeProvider();
            //2>声明编译器参数
            Parameters = new CompilerParameters();
            Parameters.GenerateExecutable = false;
            Parameters.GenerateInMemory = true;
            ErrInfo.Clear();
        }

        /// <summary>
        /// 编译，并动态执行类的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="codeSB"></param>
        /// <param name="classTypeName"></param>
        /// <param name="funcName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T CompilerCode<T>(StringBuilder codeSB, string classTypeName, string funcName, object[] param)
        {
            
            try
            {
                //3>动态编译
                CompilerResults result = Provider.CompileAssemblyFromSource(Parameters, codeSB.ToString());
                if (result.Errors.Count > 0)
                {
                    foreach (var item in result.Errors)
                    {
                        ErrInfo.Add(item.ToString());
                    }
                    return default(T);
                }
                //4>如果编译没有出错，此刻已经生成动态程序集
                //5>开始C#映射
                Assembly assembly = result.CompiledAssembly;
                object obj = assembly.CreateInstance(classTypeName);
                Type type = assembly.GetType(classTypeName);
                //6>获取对象方法
                MethodInfo method = type.GetMethod(funcName);
                //函数输入参数
                object[] objParameters = param;
                //唤醒对象，执行行为
                Object iResult = method.Invoke(obj, objParameters);
                return (T)iResult;
            }
            catch (System.NotImplementedException ex)
            {
                ErrInfo.Add(ex.Message);
            }
            catch (System.ArgumentException ex)
            {
                ErrInfo.Add(ex.Message);
            }
            catch (Exception ex)
            {
                ErrInfo.Add(ex.Message);
            }
            return default(T);
        }
    }
}
