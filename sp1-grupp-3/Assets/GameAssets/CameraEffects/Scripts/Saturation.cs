using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[SerializeField]
[PostProcess(typeof(SaturationRenderer), PostProcessEvent.AfterStack, "Custom/Saturation")]
public sealed class Saturation : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Saturation Effect Intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };


}
public sealed class SaturationRenderer : PostProcessEffectRenderer<Saturation>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Saturation"));
        sheet.properties.SetFloat("_Saturation", settings.blend);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}