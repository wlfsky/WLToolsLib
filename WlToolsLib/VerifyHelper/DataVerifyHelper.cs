/*
 * 验证实体
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WlToolsLib;
using WlToolsLib.DataShell;

namespace WlToolsLib.VerifyHelper
{
    public class DataVerifyHelper
    {
    }

    /// <summary>
    /// 范围限定
    /// </summary>
    public class LengthLimited
    {
        public LengthLimited(int min, int max)
        {
            MinLen = min;
            MaxLen = max;
        }
        public int MinLen { get; set; }
        public int MaxLen { get; set; }
    }

    /*
     * "AddNewDepartment" 调用方法名触发 验证
     * DepartmentBaseCheck 验证接口实现类
     * 字段，类型，字段说明，预处理， 会导致中断 的检测（失败跳出）空检测，长度检测（min-max），值范围检测，格式检测
     */

    #region --验证规划--
    /// <summary>
    /// 验证规划 接口，一个字段就会有一个验证计划，一个验证规划有数个验证器 验证每个具体验证项
    /// </summary>
    /// <typeparam name="TCheckData"></typeparam>
    public interface ICheckPlan<TCheckData>
    {
        TCheckData DataValue { get; }
        string Remark { get; set; }
        ICheckPlan<TCheckData> AddVerify(IValidator<TCheckData> verifyItem);
        List<IValidator<TCheckData>> Validators { get; set; }
        DataShell<string> DoVerify();
    }

    /// <summary>
    /// 基础验证计划 实体类
    /// </summary>
    /// <typeparam name="TCheckData"></typeparam>
    public class BaseCheckPlan<TCheckData> : ICheckPlan<TCheckData>
    {
        public BaseCheckPlan(TCheckData data, string remark)
        {
            if (data == null) throw new NoModelCanCheckException();
            this.DataValue = data;
            this.Remark = remark;
            this.Validators = new List<IValidator<TCheckData>>();
        }

        public TCheckData DataValue { get; private set; }
        public string Remark { get; set; }
        public Type DataType { get; set; }

        public List<IValidator<TCheckData>> Validators { get; set; }

        public ICheckPlan<TCheckData> AddVerify(IValidator<TCheckData> verifyItem)
        {
            verifyItem.VerifyPlan = this;
            Validators.Add(verifyItem);
            //// 返回自己，可再次增加验证器，方便使用。模仿js的 连环调用 do().do1().do2() 结构
            return this;
        }

        public DataShell<string> DoVerify()
        {
            if (Validators != null && Validators.Count > 0)
            {
                foreach (var item in Validators)
                {
                    var rd = item.Verify();
                    if (rd.Success == false)
                    {
                        return DataShell<string>.CreateFail<string>(rd.Info);
                    }
                }
                return DataShell<string>.CreateSuccess<string>();
            }
            return DataShell<string>.CreateSuccess<string>();
        }
    }

    #endregion

    #region --验证规划工厂--
    /// <summary>
    /// 验证工厂接口
    /// </summary>
    /// <typeparam name="TCheckData"></typeparam>
    public interface ICheckPlanFactory<TCheckData>
    {
        ICheckPlan<TCheckData> CreateCheckPlan();
    }

    /// <summary>
    /// 验证创建工厂
    /// </summary>
    /// <typeparam name="TCheckData"></typeparam>
    public class CheckPlanFactory<TCheckData> : ICheckPlanFactory<TCheckData>
    {
        private TCheckData _data;
        private string _remark;
        public CheckPlanFactory(TCheckData checkData, string remark)
        {
            _data = checkData;
            _remark = remark;
        }
        public ICheckPlan<TCheckData> CreateCheckPlan()
        {
            return new BaseCheckPlan<TCheckData>(_data, _remark);
        }
    }
    #endregion

    #region --单项验证接口 具体验证实现--
    /// <summary>
    /// 验证器接口，最小验证单元，每个数据可能有多个验证器（空验证，长度验证，范围验证）
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface IValidator<TData>
    {
        string ValidatorName { get; }
        ICheckPlan<TData> VerifyPlan { get; set; }
        DataShell<string> Verify();
    }
    #region --字符串null空字符串验证--
    /// <summary>
    /// 系统定义的空和空字符串验证
    /// </summary>
    public class NullAndEmptyValidator : IValidator<string>
    {
        public NullAndEmptyValidator()
        {
            ValidatorName = "空字符串验证";
        }
        public string ValidatorName { get; private set; }
        public ICheckPlan<string> VerifyPlan { get; set; }
        public DataShell<string> Verify()
        {
            if (string.IsNullOrWhiteSpace(VerifyPlan.DataValue))
            {
                return DataShell<string>.CreateFail<string>(string.Format("{0} 失败 : {1}", ValidatorName, VerifyPlan.Remark));
            }
            return DataShell<string>.CreateSuccess<string>();
        }
    }
    #endregion
    #region --字符串null验证--
    /// <summary>
    /// 空验证
    /// </summary>
    public class NullValidator : IValidator<string>
    {
        public NullValidator()
        {
            ValidatorName = "Null验证";
        }
        public string ValidatorName { get; private set; }
        public ICheckPlan<string> VerifyPlan { get; set; }
        public DataShell<string> Verify()
        {
            if (VerifyPlan.DataValue == null)
            {
                return DataShell<string>.CreateFail<string>(string.Format("{0} 失败 : {1}", ValidatorName, VerifyPlan.Remark));
            }
            return DataShell<string>.CreateSuccess<string>();
        }
    }
    #endregion
    #region --字符串长度验证--
    /// <summary>
    /// 长度验证，范围
    /// </summary>
    public class LengthValidator : IValidator<string>
    {
        public LengthValidator(int min, int max)
            :this(new LengthLimited(min, max))
        {
        }
        public LengthValidator(LengthLimited limit)
        {
            ValidatorName = "长度验证";
            LenLimited = limit;
        }
        public LengthLimited LenLimited { get; set; }
        public string ValidatorName { get; private set; }
        public ICheckPlan<string> VerifyPlan { get; set; }
        public DataShell<string> Verify()
        {
            int name_len = VerifyPlan.DataValue.Length;
            if (name_len < LenLimited.MinLen || name_len > LenLimited.MaxLen)
            {
                return DataShell<string>.CreateFail<string>(string.Format("{0} 失败 : {1}({3}-{4}) - 当前 : [{2}]", ValidatorName, VerifyPlan.Remark, name_len, LenLimited.MinLen, LenLimited.MaxLen));
            }
            return DataShell<string>.CreateSuccess<string>();
        }
    }
    #endregion
    #region --数字值范围验证--
    /// <summary>
    /// 数值范围验证，int
    /// </summary>
    public class IntRangeValidator : IValidator<int>
    {
        public IntRangeValidator(int min, int max)
            : this(new LengthLimited(min, max))
        {

        }

        public IntRangeValidator(LengthLimited limit)
        {
            ValidatorName = "数值范围验证";
            LenLimited = limit;
        }

        public LengthLimited LenLimited { get; set; }
        public string ValidatorName { get; private set; }
        public ICheckPlan<int> VerifyPlan { get; set; }
        public DataShell<string> Verify()
        {
            if (VerifyPlan.DataValue < LenLimited.MinLen || VerifyPlan.DataValue > LenLimited.MaxLen)
            {
                return DataShell<string>.CreateFail<string>(string.Format("{0} 失败 : {1} ({2}-{3}) 当前 [{4}]", ValidatorName, VerifyPlan.Remark, LenLimited.MinLen, LenLimited.MaxLen, VerifyPlan.DataValue));
            }
            return DataShell<string>.CreateSuccess<string>();
        }
    }
    #endregion
    #region --正则验证--
    /// <summary>
    /// 正则验证
    /// </summary>
    public class RegularValidator : IValidator<int>
    {
        public RegularValidator(string validatorName, string regularStr, string checkStr)
        {
            ValidatorName = validatorName;
            RegularStr = regularStr;
            CheckStr = checkStr;
        }
        public string RegularStr { get; protected set; }
        public string CheckStr { get; protected set; }
        public string ValidatorName { get; private set; }
        public ICheckPlan<int> VerifyPlan { get; set; }
        public DataShell<string> Verify()
        {
            Regex rg = new Regex(RegularStr);
            if (rg.IsMatch(CheckStr))
                return DataShell<string>.CreateSuccess<string>();
            else
                return DataShell<string>.CreateFail<string>(string.Format("{0} 失败 : {1} （{2}）", ValidatorName, VerifyPlan.Remark, "正则不匹配"));
        }
    }
    #endregion
    #endregion

    #region --检测预处理 接口 可能废弃--
    //public interface IPretreatment<TCheckData>
    //{
    //    TCheckData CheckData { get; set; }
    //    void Pretreatment();
    //}
    //public class BasePretreatment<TCheckData> : IPretreatment<TCheckData>
    //{
    //    public BasePretreatment(TCheckData data, Action<TCheckData> pretreatmentFunc)
    //    {
    //        OuterPretreatmentFunc = pretreatmentFunc;
    //        CheckData = data;
    //    }
    //    public TCheckData CheckData { get; set; }
    //    public Action<TCheckData> OuterPretreatmentFunc { get; protected set; }
    //    public void Pretreatment()
    //    {
    //        if (OuterPretreatmentFunc != null)
    //        {
    //            OuterPretreatmentFunc(CheckData);
    //        }
    //    }
    //}
    #endregion

    #region --检测接口--
    #region --新 验证计划版本--
    #region --基础验证类--
    /// <summary>
    /// 基础抽象验证，只完成基本关联，必须实现具体方法
    /// ----注意：继承者必须在初始化函数中使用base初始化函数，base已经执行预处理步骤
    /// </summary>
    /// <typeparam name="TCheckData"></typeparam>
    public abstract class BaseCheck<TCheckData> : ICheck<TCheckData>
    {
        /// <summary>
        /// 初始化，执行了预处理步骤，继承者必须有带base初始化
        /// </summary>
        /// <param name="currentData"></param>
        public BaseCheck(TCheckData currentData)
        {
            if (currentData == null) throw new NoModelCanCheckException();
            CurrentModel = currentData;
            Preprocessor();
        }

        /// <summary>
        /// 验证数据
        /// </summary>
        public TCheckData CurrentModel { get; protected set; }

        /// <summary>
        /// 验证方法具体实现
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public virtual DataShell<TResult> CheckModel<TResult>()
        {
            DataShell<TResult> checkVreifyResult = DataShell<TResult>.CreateSuccess<TResult>();
             checkVreifyResult = PublicCheckModel<TResult>();
            if (checkVreifyResult.Success == false)
                return checkVreifyResult;

            checkVreifyResult = CustomCheckModel<TResult>();
            if (checkVreifyResult.Success == false)
                return checkVreifyResult;

            return checkVreifyResult;
        }

        /// <summary>
        /// 公共验证，所有数据实体都需要的验证
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public virtual DataShell<TResult> PublicCheckModel<TResult>()
        {
            DataShell<TResult> checkVreifyResult = DataShell<TResult>.CreateSuccess<TResult>();
            return checkVreifyResult;
        }

        /// <summary>
        /// 自定义验证，用于每个自定验证
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public abstract DataShell<TResult> CustomCheckModel<TResult>();

        /// <summary>
        /// 预处理步骤，可自定义
        /// </summary>
        public virtual void Preprocessor()
        {
        }
    } 
    #endregion
    #endregion

    #region --检测接口--
    /// <summary>
    /// 检测接口
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface ICheck<TModel>
    {
        /// <summary>
        /// 主检测对象
        /// </summary>
        TModel CurrentModel { get; }

        /// <summary>
        /// 主检测方法
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        DataShell<TResult> CheckModel<TResult>();

        /// <summary>
        /// 公共验证，所有数据实体都需要的验证
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        DataShell<TResult> PublicCheckModel<TResult>();

        /// <summary>
        /// 自定义验证，用于每个自定验证
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        DataShell<TResult> CustomCheckModel<TResult>();

        #region --预处理方法--
        /// <summary>
        /// 预处理器
        /// </summary>
        void Preprocessor();
        #endregion
    }
    #endregion
    #endregion

    #region --老的  检测基类，有大量基础检测方法--

    /// <summary>
    /// 基础验证，暂无用武之地，其中的正则表达式可利用
    /// </summary>
    public class BaseCheck
    {
        #region --辅助检查委托--
        //Func<Func<bool>, string, ResultData<TResult>> StrNullCk<TResult> = (cn, info, rt) => { 
        //    if (cn() == true) 
        //        return RtFl();
        //};
        //Func<string, bool> StrNullCk = (s) => { return string.IsNullOrWhiteSpace(s); };//只检查字符串为空 
        //Func<string, ResultData<TResult>> RtFl<TResult> = (s)=>{ return ResultDataCreator.InitDefaultFailed<TResult>(s); };
        #endregion

        #region --格式化委托--
        Func<string, string> empty_filter = (string str) =>
        {
            if (str != null)
                return str.Trim();
            else
                return string.Empty;
        };
        Func<string, string> emptyline_filter = (string str) =>
        {
            return str.Replace("\r", "").Replace("\n", "").Trim();
        };
        #endregion

        #region --验证委托--
        /// <summary>
        /// 性别转换
        /// </summary>
        Func<string, int> sex_trans = (string str) =>
        {
            if (str == "男") return 1;
            if (str == "女") return 2;
            else return 0;
        };

        /// <summary>
        /// 名字验证，字母数字中文，1-12位允许全角半角括号
        /// </summary>
        Func<string, bool> verify_name = (string str) =>
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            Regex regex = new Regex("^[a-zA-Z0-9\\u4e00-\\u9fa5\\(\\)（）]{1,12}$");
            return regex.IsMatch(str);
        };

        /// <summary>
        /// 验证号码
        /// </summary>
        Func<string, bool> verify_phone_str = (string str) =>
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            Regex regex = new Regex("^1[0-9]{10}$");
            return regex.IsMatch(str);
        };

        /// <summary>
        /// 验证身份证号码
        /// </summary>
        Func<string, bool> verify_idstr = (string str) =>
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            Regex regex = new Regex("^[0-9]{17}[0-9X]{1}$");
            return regex.IsMatch(str);
        };

        /// <summary>
        /// 验证邮件
        /// </summary>
        Func<string, bool> verify_email = (string str) =>
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            Regex regex = new Regex("^[a-zA-Z0-9-_]+@[a-zA-Z0-9-_]+(\\.[a-zA-Z0-9-_]{2,6})+$");
            return regex.IsMatch(str);
        };

        /// <summary>
        /// 验证生日
        /// </summary>
        Func<string, bool> verify_brithday = (string str) =>
        {
            DateTime dt = new DateTime();
            return DateTime.TryParse(str, out dt);
        };

        /// <summary>
        /// 第二个验证生日
        /// </summary>
        Func<string, bool> verify_brithday2 = (string str) =>
        {
            if (string.IsNullOrWhiteSpace(str)) return false;
            Regex regex = new Regex("^[0-9]{4}-[0-9]{2}-[0-9]{2}$");
            return regex.IsMatch(str);
        };
        #endregion
    }
    #endregion

    #region --自定义异常（没有数据模型供检测）--
    public class NoModelCanCheckException : Exception
    {
        public NoModelCanCheckException()
            : base("没有数据可以检测")
        {
        }
    }
    #endregion
}
