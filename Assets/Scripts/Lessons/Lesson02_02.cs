using KarelTheRobotUnity.Core;

namespace TestNamespace
{
    public class Lesson02_02 : Lesson02_02_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо написать три алгоритма поведения для робота, каждый из которых должен доставить бипер к указанной цели
            //
            // Необходимые команды:
            //
            // Move();
            // TurnRight();
            // TurnLeft();
            // TakeBeeper();
            // PutBeeper();
            // 
            // Также вам понадобится использовать выражения:
            // if (условие) 
            // {
            //     код 
            // }
            // else
            // {
            //     код else
            // }
            //
            // switch (переменная)
            // {
            //     case значение1:
            //         код;
            //         break;
            //     case значение2:
            //         код;
            //         break;
            //     default:
            //         throw new System.Exception("Wrong index = " + переменная);
            // }

            int target = GetTargetIndex(); // Используйте эту переменную в if - else if, а затем в switch


        }
    }
}
