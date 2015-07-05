using System;
using System.Collections.Generic;
using System.Text;

namespace CBSM.Domain
{
    /// <summary>
    /// Holds the information of a scene
    /// </summary>
    class Scene
    {
        #region "Fields"

        private int id;
        private Location location;
        private DateTime time;
        private string mood;
        private string description;
        private TimeSpan aproxSceneDuration;
        private int priority;
        private List<Attribute> attributes;
        private List<Staff> staff;
        private List<Character> characters;
        private List<Fragment> fragments;

        #endregion

        #region "Constructors"
        #endregion

        #region "Properties"

        /// <summary>
        /// Gets or sets the location at wich the scene needs to be filmed
        /// </summary>
        public Location Location
        {
            get { return this.location; }
            set { this.location = value; }
        }

        /// <summary>
        /// Gets or sets the date and time of the scene in the fantasy world
        /// </summary>
        public DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        /// <summary>
        /// Gets or sets the mood of the scene
        /// </summary>
        public string Mood
        {
            get { return this.mood; }
            set { this.mood = value; }
        }

        /// <summary>
        /// Gets or sets the (short) description of the scene
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        /// <summary>
        /// Gets or sets the length of the scene
        /// </summary>
        public TimeSpan AproxSceneDuration
        {
            get { return this.aproxSceneDuration; }
            set { this.aproxSceneDuration = value; }
        }

        /// <summary>
        /// Gets or sets the priority of the scene (???)
        /// </summary>
        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        /// <summary>
        /// Gets or sets the attributes used in the scene
        /// </summary>
        public List<Attribute> Attributes
        {
            get { return new List<Attribute>(this.attributes); }
            set { this.attributes = value; }
        }
        
        /// <summary>
        /// Gets or sets the people of the staff in this scene
        /// </summary>
        public List<Staff> Staff
        {
            get { return new List<Staff>(this.staff); }
            set { this.staff = value; }
        }

        /// <summary>
        /// Gets or sets the characters that apear in the scene
        /// </summary>
        public List<Character> Characters
        {
            get { return this.characters; }
            set { this.characters = value; }
        }

        /// <summary>
        /// Gets or sets the fragments of video made for this scene
        /// </summary>
        public List<Fragment> Fragments
        {
            get { return this.fragments; }
            set { this.fragments = value; }
        }

        #endregion

        #region "Methods"
        #endregion
    }
}
