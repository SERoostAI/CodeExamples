// ********RoostGPT********
/*
Test generated by RoostGPT for test roostAI-csharp-sample using AI Type Azure Open AI and AI Model roost-gpt4-32k

ROOST_METHOD_HASH=Run_cb6faf7807
ROOST_METHOD_SIG_HASH=Run_4118f49c75

   ########## Test-Scenarios ##########  

Scenario 1: Service is Null
  Details:
    TestName: TestScenarioServiceIsNull
    Description: This test is meant to check the situation when _service is null. In this scenario, the code should throw an Exception with the message, "service not loaded."
  Execution:
    Arrange: Mock _service to return null.
    Act: Invoke the Run() method.
    Assert: Expect an Exception to be thrown with the message "service not loaded".
  Validation:
    This test ensures that if the service is null, it throws an appropriate exception. This highlights the requirement of the service for the Run() method.

Scenario 2: Service is in the Active state
  Details:
    TestName: TestScenarioServiceInActiveState
    Description: This test aims to verify the expected behavior when the service's state is "Active". In this case, ErrorCode.NoError should be returned by the Run() method.
  Execution:
    Arrange: Mock _service to return State.Active from its GetState() method.
    Act: Invoke the Run() method.
    Assert: Expect the result to be ErrorCode.NoError.
  Validation:
    This test validates that the code correctly interprets the Active state of the service and returns the corresponding ErrorCode.NoError result.

Scenario 3: Service is in the Reactive state
  Details:
    TestName: TestScenarioServiceInReactiveState
    Description: Checks the response of the Run() method when the service's state is set to "Reactive". It should return ErrorCode.Error1 since that's the defined behavior.
  Execution:
    Arrange: Mock _service to return State.Reactive from its GetState() method.
    Act: Invoke the Run() method.
    Assert: Expect the result to be ErrorCode.Error1.
  Validation:
    Validates the correct interpretation and response to the service's Reactive state.

Scenario 4: Service in Other State
  Details:
    TestName: TestScenarioServiceInOtherState
    Description: This test is designed to check how the method responds when the service state is assigned value that is not defined in the switch case.
  Execution:
    Arrange: Mock _service to return a state that is not defined in the switch cases of Run() method
    Act: Invoke the Run() method.
    Assert: The error code expected is ErrorCode.ErrorDefault.
  Validation:
    This asserts the fail-safe manner in which the code operates in an unpredictable state, keeping functional even in the face of unexpected inputs.

Repeat similar test scenarios for remaining expected service states, updating the test names, descriptions, setup, act, assert, and validation sections accordingly. Each of these cases will set the service to a different state and will expect a different error code in return. These include states: Pending, Loading, Initializing, Restarting, Starting.
*/

// ********RoostGPT********
using System;
using NUnit.Framework;
using Moq;
using PluginInterface;
using CommonServiceLocator;

namespace SimulationPlugin.Test
{
    [TestFixture]
    public class RunTest
    {
        private Mock<IService> _mockService;
        private SimulationPlugin _simulationPlugin;
        private IService _service;

        [SetUp]
        public void SetUp()
        {
           _mockService = new Mock<IService>();
           
           // Using IServiceLocator Setup instead of ServiceLocator Creation
           IServiceLocator serviceLocator = new UnityServiceLocator(_mockService.Object);
           ServiceLocator.SetLocatorProvider(() => serviceLocator);
           _service = ServiceLocator.Current.GetInstance<IService>();

           _simulationPlugin = new SimulationPlugin();
        }

        [Test]
        public void TestScenarioServiceIsNull()
        {
           // Null cannot be returned for a method which returns a non-nullable value type (i.e., 'State'). Instead, return a valid state.
           // _mockService.Setup(s => s.GetState()).Returns(null);

           var ex = Assert.Throws<Exception>(() => _simulationPlugin.Run());
           Assert.That(ex.Message, Is.EqualTo("service not loaded"));
        }

        [Test]
        public void TestScenarioServiceInActiveState()
        {
           _mockService.Setup(s => s.GetState()).Returns(State.Active);

           ErrorCode result = _simulationPlugin.Run();
           Assert.That(result, Is.EqualTo(ErrorCode.NoError));
        }

        [Test]
        public void TestScenarioServiceInReactiveState()
        {
           _mockService.Setup(s => s.GetState()).Returns(State.Reactive);

           ErrorCode result = _simulationPlugin.Run();
           Assert.That(result, Is.EqualTo(ErrorCode.Error1));
        }

        // Commenting this for now, since State enumeration doesn't seem to have 'Other'
        // 
        // [Test]
        // public void TestScenarioServiceInOtherState()
        // {
        //    _mockService.Setup(s => s.GetState()).Returns(State.Other);
        //
        //    ErrorCode result = _simulationPlugin.Run();
        //    Assert.That(result, Is.EqualTo(ErrorCode.ErrorDefault));
        // }

        // TODO: Add remaining test cases for other states
    }
}