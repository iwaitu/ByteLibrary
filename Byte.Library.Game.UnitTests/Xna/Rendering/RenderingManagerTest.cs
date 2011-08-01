using System.Collections.Generic;
using System.Linq;
using Byte.Library.Game.UnitTests.Xna.Helpers;
using Byte.Library.Game.Xna.Rendering;
using Xunit;

namespace Byte.Library.Game.UnitTests.Xna.Rendering
{
    public class RenderingManagerTest
    {
        [Fact]
        public void New_RenderingManager_has_no_renderers_attached()
        {
            var renderingManager = new RenderingManager<TestEnum>();

            Assert.Empty(renderingManager.GetRenderersForState(TestEnum.Alpha));
            Assert.Empty(renderingManager.GetRenderersForState(TestEnum.Beta));
        }

        [Fact]
        public void Renderer_can_be_attached_to_RenderingManager()
        {
            var renderingManager = new RenderingManager<TestEnum>();

            var renderer = new TestRenderer();
            renderingManager.AttachRenderer(TestEnum.Alpha, renderer);

            IEnumerable<IRenderer> allRenderers = renderingManager.Renderers;
            IEnumerable<IRenderer> alphaRenderers = renderingManager.GetRenderersForState(TestEnum.Alpha);

            Assert.Contains(renderer, allRenderers);
            Assert.Contains(renderer, alphaRenderers);
        }

        [Fact]
        public void GetRenderersForState_returns_correct_renderers_for_given_state()
        {
            var alphaRenderer = new TestRenderer();
            var betaRenderer = new TestRenderer();

            var renderingManager = new RenderingManager<TestEnum>();
            renderingManager.AttachRenderer(TestEnum.Alpha, alphaRenderer);
            renderingManager.AttachRenderer(TestEnum.Beta, betaRenderer);

            Assert.Contains(alphaRenderer, renderingManager.GetRenderersForState(TestEnum.Alpha));
            Assert.DoesNotContain(alphaRenderer, renderingManager.GetRenderersForState(TestEnum.Beta));
            Assert.Contains(betaRenderer, renderingManager.GetRenderersForState(TestEnum.Beta));
            Assert.DoesNotContain(betaRenderer, renderingManager.GetRenderersForState(TestEnum.Alpha));
        }

        [Fact]
        public void SetVisibleState_appropriately_sets_visibility_of_renderers()
        {
            var alphaRenderer = new TestRenderer();
            var betaRenderer = new TestRenderer();

            var renderingManager = new RenderingManager<TestEnum>();
            renderingManager.AttachRenderer(TestEnum.Alpha, alphaRenderer);
            renderingManager.AttachRenderer(TestEnum.Beta, betaRenderer);

            Assert.True(renderingManager.Renderers.All(r => r.Visible == false));
            Assert.Equal(default(TestEnum), renderingManager.VisibleState);

            renderingManager.SetVisibleState(TestEnum.Beta);

            Assert.Equal(TestEnum.Beta, renderingManager.VisibleState);
            Assert.True(renderingManager.GetRenderersForState(TestEnum.Alpha).All(r => r.Visible == false));
            Assert.True(renderingManager.GetRenderersForState(TestEnum.Beta).All(r => r.Visible == true));
        }
    }
}
