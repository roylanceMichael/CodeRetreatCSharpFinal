using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using Rhino.Mocks;

namespace GameOfLife
{
	[TestFixture]
	public class GameOfLifeTests
	{
		[TestCase(true, 0, false)]
		[TestCase(true, 2, true)]
		[TestCase(true, 3, true)]
		[TestCase(true, 4, false)]
		[TestCase(false, 2, false)]
		[TestCase(false, 3, true)]
		[TestCase(false, 4, false)]
		public void RulesTests(bool startState, int numberOfNeighbors, bool expectedState)
		{
			Assert.That(NextState(startState, numberOfNeighbors), Is.EqualTo(expectedState));
		}

		[Test]
		public void EmptyBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point>();
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(0));
		}

		[Test]
		public void OneCellBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(0, 1) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(1));
		}

		[Test]
		public void TwoCellYBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(0, 1), new Point(0, -1) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(2));
		}

		[Test]
		public void TwoCellXBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(1, 0), new Point(-1, 0) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(2));
		}

		[Test]
		public void FourCellBeyondNeighborsBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(2, 0), new Point(-2, 0), new Point(0, 2), new Point(0, -2) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(0));
		}

		[Test]
		public void FourCellCornerNeighborsBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(1, 1), new Point(-1, -1), new Point(1, -1), new Point(-1, 1) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(4));
		}

		[Test]
		public void FourCellNeighborsBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(1, 2), new Point(-1, -1), new Point(1, -1), new Point(-1, 1) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(3));
		}

		[Test]
		public void SelfNeighborsBoardNeighborTest()
		{
			var cell = new Point(0, 0);
			var world = new List<Point> { new Point(0, 0) };
			Assert.That(CalcuateNumberOfNeighbors(world, cell), Is.EqualTo(0));
		}

		private int CalcuateNumberOfNeighbors(IEnumerable<Point> points, Point cell)
		{
			return points.Count(possibleNeighbor => !(possibleNeighbor.X == cell.X && possibleNeighbor.Y == cell.Y)
					&& (Math.Abs(possibleNeighbor.X - cell.X) <= 1 && Math.Abs(possibleNeighbor.Y - cell.Y) <= 1));
		}

		private bool NextState(bool startState, int numberOfNeighbors)
		{
			if (numberOfNeighbors == 2 && startState)
			{
				return true;
			}
			if (numberOfNeighbors == 3) return true;
			return false;
		}
	}
}