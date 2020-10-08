using Prism.Commands;

namespace VNCExplore_LearnPrism_BrianLagunas.Infrastructure
{
    public static class GlobalCommands
    {
        public static CompositeCommand SaveAllCommandCC = new CompositeCommand();
        public static CompositeCommand SaveAllCommandEA = new CompositeCommand();
        public static CompositeCommand SaveAllCommandSS = new CompositeCommand();
    }
}
