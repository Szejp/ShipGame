using System;
using System.Collections;
using System.Collections.Generic;
using DailyRewardSytem.TimeChecking;
using UnityEngine;
using NUnit.Framework;
using UnityEditor.XR.Daydream;

public class TEST_NextDayChecker
{

    [Test]
    public void CheckIfAtLeastOneDayPassed_ReturnsTrueAfterOneDay()
    {
        DateTime before = new DateTime(2000, 12, 20);
        DateTime after = new DateTime(2000, 12, 21);
        
        Assert.True(NextDayChecker.CheckIfAtLeastOneDayPassed(before, after));
    }

    [Test]
    public void CheckIfAtLeastOneDayPassed_ReturnsTrueAfter2hButDifferentDays()
    {
        DateTime before = new DateTime(2000, 12, 20, 23 , 0, 0);
        DateTime after = new DateTime(2000, 12, 21, 1 , 0, 0);
        
        Assert.True(NextDayChecker.CheckIfAtLeastOneDayPassed(before, after));
    } 
    
    [Test]
    public void CheckIfAtLeastOneDayPassed_ReturnsFalseAfter1hSameDay()
    {
        DateTime before = new DateTime(2000, 12, 20, 1 , 0, 0);
        DateTime after = new DateTime(2000, 12, 20, 2 , 0, 0);
        
        Assert.False(NextDayChecker.CheckIfAtLeastOneDayPassed(before, after));
    }
    
    [Test]
    public void CheckIfAtLeastOneDayPassed_ReturnsFalseIfOneDateBeforeOther()
    {
        DateTime before = new DateTime(2000, 12, 20, 1 , 0, 0);
        DateTime after = new DateTime(2000, 12, 19, 0 , 0, 0);
        
        Assert.False(NextDayChecker.CheckIfAtLeastOneDayPassed(before, after));
    }
}

public class TEST_RetentionRewardTimeSystem
{
    [Test]
    public void FirstRewardIsClaimableAtStart()
    {
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        
        Assert.True(SUT.RewardCalendarData.CanNextRewardBeClaimed);
    }
    
    [Test]
    public void ThereIs0RewardsClaimedAtStart()
    {
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        
        Assert.True(SUT.RewardCalendarData.RewardsClaimed == 0);
    }
    
    [Test]
    public void AfterClaimAnotherRewardCantBeClaimedStraightAway()
    {
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        SUT.Claim();
        
        Assert.False(SUT.RewardCalendarData.CanNextRewardBeClaimed);
    }
    
    [Test]
    public void ThereIsOneRewardClaimedAfrerFirstClaim()
    {
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        SUT.Claim();
        
        Assert.True(SUT.RewardCalendarData.RewardsClaimed == 1);
    }
    
    [Test]
    public void ClearAllData_Works()
    {
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        SUT.Claim();
        SUT.ClearAllData();
        
        Assert.True(SUT.RewardCalendarData.RewardsClaimed == 0);
        Assert.True(SUT.RewardCalendarData.CanNextRewardBeClaimed);

    }
    [Test]
    public void DataIsCorectAfter40Tries()
    {
        DateTime dateTime = new DateTime(2005, 12, 12, 2, 12 ,12);
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        fakeDateProvider.SetDate(dateTime);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        for (int i = 0; i < 40; i++)
        {
            dateTime = dateTime.AddDays(1);
            fakeDateProvider.SetDate(dateTime);
            SUT.Claim();
        }
        
        Assert.True(SUT.RewardCalendarData.RewardsClaimed == 40);
    }
    
    [Test]
    public void UsingClaimMultipleTimesIsPrevented()
    {
        DateTime dateTime = new DateTime(2005, 12, 12, 2, 12 ,12);
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        fakeDateProvider.SetDate(dateTime);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        
        SUT.Claim();
        SUT.Claim();
        SUT.Claim();
        
        Assert.True(SUT.RewardCalendarData.RewardsClaimed == 1);
    }
    
    [Test]
    public void ClaimReturnsFalseWhenUsedMultipleTimes()
    {
        DateTime dateTime = new DateTime(2005, 12, 12, 2, 12 ,12);
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        fakeDateProvider.SetDate(dateTime);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        
        SUT.Claim();

        Assert.False(SUT.Claim());
    } 
    
    [Test]
    public void ClaimReturnsTrueFirstTime()
    {
        DateTime dateTime = new DateTime(2005, 12, 12, 2, 12 ,12);
        FakeDateProvider fakeDateProvider = new FakeDateProvider(6, 15, 4);
        fakeDateProvider.SetDate(dateTime);
        RetentionRewardTimeSystem SUT = new RetentionRewardTimeSystem("testKey", fakeDateProvider);
        SUT.ClearAllData();
        
        Assert.True(SUT.Claim());
    }
    
}
