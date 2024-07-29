#if TOOLS
using Godot;
using SimpleUnitTests;
using System;

[Tool]
public partial class SimpleUnitTestsEditor : EditorPlugin
{
    protected Control Dock { get; set; }
    protected Button Button { get; set; }
    protected TestRunner TestRunner { get; set; }
    protected Tree TestResults { get; set; }
    protected TreeItem TreeRoot { get; set; }
    protected TreeItem CurrentTestSuite { get; set; }

    public override void _EnterTree()
	{
        Dock = (Control) GD.Load<PackedScene>("res://addons/simpleunittests/ui/simple_unit_tests.tscn").Instantiate();
        AddControlToDock(DockSlot.LeftUl, Dock);

        // Connect button
        Button = (Button) Dock.GetNode("Main layout/btn_run");
        TestResults = (Tree) Dock.GetNode("Main layout/tr_results");

        TestResults.SetColumnCustomMinimumWidth(0, 30);
        TestResults.SetColumnCustomMinimumWidth(1, 270);
        TestResults.SetColumnClipContent(0, false);
        TestResults.SetColumnClipContent(1, true);

        Button.Pressed += OnPressed;

        TestRunner = new TestRunner();
        TestRunner.TestStarted += OnTestStarted;
        TestRunner.TestFinished += OnTestFinished;
        TestRunner.TestFailed += OnTestFailed;
        TestRunner.TestSucceeded += OnTestSucceeded;

        TestRunner.TestRunnerFinished += OnTestRunnerFinished;
        TestRunner.TestRunnerStarted += OnTestRunnerStarted;
    }

    private Texture2D GetEditorIcon(string name, string theme_name = "EditorIcons")
    {
        var theme = EditorInterface.Singleton.GetBaseControl().Theme;
        var icon = theme.GetIcon(name, theme_name);

        return icon;
    }

    public override void _ExitTree()
	{
        RemoveControlFromDocks(Dock);
        Dock.Free();
    }

    private void OnPressed()
    {
        TestRunner.Run();
    }

    private void OnTestRunnerStarted(object sender, EventArgs e)
    {
        TestResults.Clear();
        TreeRoot = TestResults.CreateItem();
        TestResults.HideRoot = true;
    }

    private void OnTestStarted(object sender, TestEvent e)
    {
        CurrentTestSuite = TreeRoot.CreateChild();
        CurrentTestSuite.SetText(0, e.TestSuite.Name);
    }

    private void OnTestFinished(object sender, TestEvent e)
    {
        var text = string.Format("{0} finished with {1} tests.", e.TestSuite.Name, e.TestSuite.NoOfTests);
        GD.Print(text);
    }

    private void OnTestSucceeded(object sender, TestMethodEvent e)
    {
        var item = CurrentTestSuite.CreateChild();

        item.SetIcon(0, GetEditorIcon("StatusSuccess"));
        item.SetText(1, e.Method.Name);       
    }

    private void OnTestFailed(object sender, TestFailedEvent e)
    {
        var item = CurrentTestSuite.CreateChild();

        item.SetIcon(0, GetEditorIcon("StatusError"));
        item.SetText(1, e.Method.Name);

        var ex = e.Exception;
        item = item.CreateChild();
        item.SetIcon(0, GetEditorIcon("StatusError"));
        item.SetText(1, string.Format($"In {ex.File} line {ex.Line}: {ex.Message}."));
    }

    private void OnTestRunnerFinished(object sender, EventArgs e) 
    {
        GD.Print("Test runner finished.");
    }
}
#endif
