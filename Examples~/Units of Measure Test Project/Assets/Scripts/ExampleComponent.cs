using Software10101.Units;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExampleComponent : MonoBehaviour {
    [Header(nameof(Duration))]
    public Duration DurationValue;
    public Duration[] DurationArray;
    public List<Duration> DurationList;
    
    [Header(nameof(Mass))]
    public Mass MassValue;
    public Mass[] MassArray;
    public List<Mass> MassList;
    
    [Header(nameof(Temperature))]
    public Temperature TemperatureValue;
    public Temperature[] TemperatureArray;
    public List<Temperature> TemperatureList;
    
    [Header(nameof(Length))]
    public Length LengthValue;
    public Length[] LengthArray;
    public List<Length> LengthList;
    
    [Header(nameof(Area))]
    public Area AreaValue;
    public Area[] AreaArray;
    public List<Area> AreaList;
    
    [Header(nameof(Volume))]
    public Volume VolumeValue;
    public Volume[] VolumeArray;
    public List<Volume> VolumeList;
    
    [Header(nameof(Speed))]
    public Speed SpeedValue;
    public Speed[] SpeedArray;
    public List<Speed> SpeedList;
    
    [Header(nameof(Density))]
    public Density DensityValue;
    public Density[] DensityArray;
    public List<Density> DensityList;
    
    [Header(nameof(Momentum))]
    public Momentum MomentumValue;
    public Momentum[] MomentumArray;
    public List<Momentum> MomentumList;
    
    [Header(nameof(VolumetricFlowRate))]
    public VolumetricFlowRate VolumetricFlowRateValue;
    public VolumetricFlowRate[] VolumetricFlowRateArray;
    public List<VolumetricFlowRate> VolumetricFlowRateList;
    
    [Header("Nested")]
    public NestedClass NestedClassValue;
}

[Serializable]
public class NestedClass {
    public Duration DurationValue;
    public Mass MassValue;
    public Temperature TemperatureValue;
    public Length LengthValue;
    public Area AreaValue;
    public Volume VolumeValue;
    public Speed SpeedValue;
    public Density DensityValue;
    public Momentum MomentumValue;
    public VolumetricFlowRate VolumetricFlowRateValue;
}
