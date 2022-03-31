using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERMapViewer
{
    internal abstract class ProgressIndicator
    {
        protected long maxIndex;
        public virtual long MaxIndex { 
            get => maxIndex;
            set {
                maxIndex = value;
                currentIndex = Math.Min(maxIndex, currentIndex);
                perUpdate = Math.Max(1, maxIndex / 1_000);
                nextMilestone = Math.Min(maxIndex, currentIndex + perUpdate);
                UpdateString();
            }
        }
        protected long currentIndex;
        protected long nextMilestone;
        public virtual long CurrentIndex {
            get => currentIndex; 
            set {
                if (currentIndex > nextMilestone) {
                    currentIndex = value;
                    while (nextMilestone <= currentIndex) nextMilestone += perUpdate;
                    UpdateString();
                } else {
                    currentIndex = value;
                }
            }
        }
        public virtual bool IsTaskIndexed { get; set; }
        protected string taskName;
        public virtual string TaskName { 
            get {
                return taskName;
            }
            set {
                taskName = value;
                UpdateString();
            }
        }
        protected long perUpdate;

        protected abstract void UpdateString(string message);
        protected virtual void UpdateString()
        {
            UpdateString(GetString());
        }
        protected virtual string GetString()
        {
            StringBuilder ans = new StringBuilder();
            ans.Append(TaskName);
            if (IsTaskIndexed) {
                ans.Append(' ').Append(CurrentIndex).Append('/').Append(MaxIndex).Append(" (");
                ans.Append($"{(float)CurrentIndex / MaxIndex*100.0:F2}%").Append(')');
            }
            return ans.ToString();
        }
        public void Finish()
        {
            UpdateString("Done.");
        }
        public ProgressIndicator(string taskName)
        {
            this.taskName = taskName;
        }
    }

    internal class DelegateProgressIndicator : ProgressIndicator
    {
        private Action<string> del;
        public Action<string> Delegate {
            get => del;
            set {
                del = value;
                UpdateString();
            }
        }
        public DelegateProgressIndicator(string taskName, Action<string> del) : base(taskName)
        {
            this.del = del;
        }
        protected override void UpdateString(string message)
        {
            Delegate(message);
        }
    }

    internal class NoOpProgressIndicator : ProgressIndicator
    {
        public NoOpProgressIndicator(string taskName) : base(taskName) { }
        protected override void UpdateString(string message) {}
    }
}
