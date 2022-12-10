#region 注 释
/***
 *
 *  Title:
 *  
 *  Description:
 *  
 *  Date:
 *  Version:
 *  Writer: 半只龙虾人
 *  Github: https://github.com/HalfLobsterMan
 *  Blog: https://www.crosshair.top/
 *
 */
#endregion
using CZToolKit.Core.ViewModel;
using CZToolKit.GraphProcessor;

namespace CZToolKit.BehaviorTree
{
    [TaskIcon("BehaviorTree/Icons/Selector")]
    [NodeTitle("选择执行")]
    [NodeTooltip("依次执行，直到Success或Running，并返回该状态")]
    [NodeMenu("Composite", "Selector")]
    public class Selector : Compsite { }

    [ViewModel(typeof(Selector))]
    public class SelectorVM : CompsiteVM
    {
        int index;

        public SelectorVM(BaseNode model) : base(model) { }

        protected override void OnStart()
        {
            base.OnStart();
            index = 0;
        }

        protected override TaskStatus OnUpdate()
        {
            for (int i = index; i < tasks.Count; i++)
            {
                var task = tasks[i];
                var tmpStatus = task.Update();
                if (tmpStatus == TaskStatus.Success)
                {
                    return TaskStatus.Success;
                }
                if (tmpStatus == TaskStatus.Running)
                {
                    return TaskStatus.Running;
                }
                index++;
            }
            return TaskStatus.Failure;
        }
    }
}