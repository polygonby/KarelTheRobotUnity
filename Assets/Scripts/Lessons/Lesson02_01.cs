using KarelTheRobotUnity.Core;

namespace TestNamespace
{
    public class Lesson02_01 : Lesson02_01_Internal
    {
        protected override void Execute()
        {
            // В данном задании необходимо написать два алгоритма поведения для робота.
            // Первый вариант должен доставлять бипер до целевой точки и отводить робота на любую ячейку
            // Второй вариант должен доводить робота до цели не трогая бипер
            //
            // Необходимые команды:
            //
            // Move();
            // TurnRight();
            // TurnLeft();
            // TakeBeeper();
            // PutBeeper();
            // 
            // Также вам понадобится использовать выражение:
            // if (условие) 
            // {
            //     код 
            // }
            // else
            // {
            //     код else
            // }

            bool isBeeperDelivery = IsBeeperDelivery(); // Используйте эту переменную в if


        }
    }
}
