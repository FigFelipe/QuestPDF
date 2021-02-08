﻿using NUnit.Framework;
using QuestPDF.Drawing.SpacePlan;
using QuestPDF.Elements;
using QuestPDF.Infrastructure;
using QuestPDF.UnitTests.TestEngine;

namespace QuestPDF.UnitTests
{
    [TestFixture]
    public class AlignmentTests
    {
        [Test]
        public void Measure_ShouldHandleNullChild() => new Alignment().MeasureWithoutChild();
        
        [Test]
        public void Draw_ShouldHandleNullChild() => new Alignment().DrawWithoutChild();

        [Test]
        public void Measure_ShouldReturnWrap_WhenChildReturnsWrap()
        {
            TestPlan
                .For(x => new Alignment
                {
                    Child = x.CreateChild("child")
                })
                .MeasureElement(new Size(1000, 500))
                .ExpectChildMeasure("child", expectedInput: new Size(1000, 500), returns: new Wrap())
                .CheckMeasureResult(new Wrap());
        }

        [Test]
        public void Draw_HorizontalCenter_VerticalCenter()
        {
            TestPlan
                .For(x => new Alignment
                {
                    Horizontal = HorizontalAlignment.Center,
                    Vertical = VerticalAlignment.Middle,
                    
                    Child = x.CreateChild("child")
                })
                .DrawElement(new Size(1000, 500))
                .ExpectChildMeasure("child", expectedInput: new Size(1000, 500), returns: new PartialRender(new Size(400, 200)))
                .ExpectCanvasTranslate(new Position(300, 150))
                .ExpectChildDraw("child", new Size(400, 200))
                .ExpectCanvasTranslate(new Position(-300, -150))
                .CheckDrawResult();
        }
        
        [Test]
        public void Draw_HorizontalLeft_VerticalCenter()
        {
            TestPlan
                .For(x => new Alignment
                {
                    Horizontal = HorizontalAlignment.Left,
                    Vertical = VerticalAlignment.Middle,
                    
                    Child = x.CreateChild("child")
                })
                .DrawElement(new Size(400, 300))
                .ExpectChildMeasure("child", expectedInput: new Size(400, 300), returns: new FullRender(new Size(100, 50)))
                .ExpectCanvasTranslate(new Position(0, 125))
                .ExpectChildDraw("child", new Size(100, 50))
                .ExpectCanvasTranslate(new Position(0, -125))
                .CheckDrawResult();
        }
        
        [Test]
        public void Draw_HorizontalCenter_VerticalBottom()
        {
            TestPlan
                .For(x => new Alignment
                {
                    Horizontal = HorizontalAlignment.Center,
                    Vertical = VerticalAlignment.Bottom,
                    
                    Child = x.CreateChild("child")
                })
                .DrawElement(new Size(400, 300))
                .ExpectChildMeasure("child", expectedInput: new Size(400, 300), returns: new FullRender(new Size(100, 50)))
                .ExpectCanvasTranslate(new Position(150, 250))
                .ExpectChildDraw("child", new Size(100, 50))
                .ExpectCanvasTranslate(new Position(-150, -250))
                .CheckDrawResult();
        }
        
        [Test]
        public void Draw_HorizontalRight_VerticalTop()
        {
            TestPlan
                .For(x => new Alignment
                {
                    Horizontal = HorizontalAlignment.Right,
                    Vertical = VerticalAlignment.Top,
                    
                    Child = x.CreateChild("child")
                })
                .DrawElement(new Size(400, 300))
                .ExpectChildMeasure("child", expectedInput: new Size(400, 300), returns: new FullRender(new Size(100, 50)))
                .ExpectCanvasTranslate(new Position(300, 0))
                .ExpectChildDraw("child", new Size(100, 50))
                .ExpectCanvasTranslate(new Position(-300, 0))
                .CheckDrawResult();
        }
    }
}