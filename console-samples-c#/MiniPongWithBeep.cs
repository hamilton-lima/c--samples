using System;
namespace Draft
{
 class MainClass
 {
  public static void Main (string[] args)
  {
   int step = 30;
   Console.CursorVisible = false;
   Scene scene = new Scene ();
   scene.start();
   while (true) {
    Console.Clear();
    scene.update ();
    scene.draw ();
    System.Threading.Thread.Sleep (step);
   }
  }
 }
 class Scene
 {
  Random random = new Random();
  int x = 0;
  int y = 0;
  int dirX = 1;
  int dirY = 1;
  char display = '*';
  public void start(){
   x = random.Next(0,Console.WindowWidth);
   y = random.Next(0,Console.WindowHeight);   
  }
  public void update ()
  {
   if( Console.KeyAvailable ){
    ConsoleKeyInfo cki = Console.ReadKey(true);
    display = cki.KeyChar;
    Console.Beep( 100, 300);
   }
   x += dirX;
   y += dirY;
   if (x >= Console.WindowWidth || x <= 0) {
    // http://msdn.microsoft.com/en-us/library/4fe3hdb1.aspx
    Console.Beep( 400, 200);
    dirX = dirX * -1;
    x += dirX;
   }
   if (y >= Console.WindowHeight || y <= 0) {
    // http://msdn.microsoft.com/en-us/library/4fe3hdb1.aspx
    Console.Beep( 200, 200);
    dirY = dirY * -1;
    y += dirY;
   }
  }
  public void draw ()
  {
   Console.SetCursorPosition (x, y);
   Console.Write ( display ); 
  }
 }
}