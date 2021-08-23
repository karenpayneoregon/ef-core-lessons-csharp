Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NorthWindCoreLibrary_vb
Imports NorthWindCoreLibrary_vb.Classes
Imports NorthWindCoreLibrary_vb.Data

<TestClass>
Public Class UnitTest1
    <TestMethod>
    Sub LoadCustomers()
        Using context = New NorthWindContext
            Dim results As List(Of CustomerItem) = CustomersOperations.CustomerProjection()

            Assert.AreEqual(results.Count, 91)

        End Using
    End Sub
End Class