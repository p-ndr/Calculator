using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator
{
	public class CustomStack<T> : IEnumerable<T>
	{

		/*
		 * This class implements a simple stack for performing Shunting Yard Algorithm, using 
		 * the predefined LinkedList class of C#. It is designed to store any input.
		 *
		 * Methods supported:
		 * - Push
		 * - Pop
		 * - Top
		 * - Size
		 * - isEmpty
		 */

		LinkedList<T> stackStorage;
		public CustomStack()
		{
			stackStorage = new LinkedList<T>(); // fields should be newed in creation only.
		}

		public void push(T e)
		{
			/*
			 * This method adds an element to the stack.
			 *
			 * @params: Generic Element
			 * @Return: void (Better return a boolean in order to make sure the element is added to the stack, but 
			 *		    let's keep things simple.
			 */
			stackStorage.AddLast(e);
		}

		public T pop()
		{
			/*
			 * This method removes the last element of the stack (as stack has a LIFO order)
			 * and returns it.
			 * 
			 * @params: none
			 * @return: The element on the top of the stack.
			 * 
			 */

			T t = stackStorage.Last.Value;
			stackStorage.RemoveLast();
			return t;
		}

		public T top()
		{
			/*
			 * This method returns the value on the top of the stack, without removing it.
			 * 
			 * @params: none
			 * @return: The element on the top of the stack
			 * 
			 */

			return stackStorage.Last.Value;
		}

		public int size()
		{

			/*
			 * This method returns the size of the stack. As with the implementation, it oughts to return
			 * the length of the linked list.
			 * 
			 * @params: none
			 * @return: The size of the stack
			 * 
			 */

			return stackStorage.Count;
		}

		public bool isEmpty()
        {

			/*
			 * This method checks whether the stack is empty or not.
			 * 
			 * @params: none
			 * @return: bool
			 * 
			 */

			return stackStorage.Count <= 0;
        }

        public IEnumerator<T> GetEnumerator()
        {

			/*
			 * Exposing the stack to an enumerator. As it is implemented with linkedlist,
			 * the enumerator of the linkedlist suffices. Implementing this function and the
			 * next function is mandatory, as this class inherits IEnumerator<> interface.
			 * 
			 */

            return ((IEnumerable<T>)stackStorage).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)stackStorage).GetEnumerator();
        }
    }
}